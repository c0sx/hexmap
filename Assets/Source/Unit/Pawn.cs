using System;
using System.Collections.Generic;
using UnityEngine;

using Map.Cell;
using Map.Movement.Selector;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Pawn : MonoBehaviour
    {
        public event Action<Pawn> Selected;
        public event Action<Pawn, GridCell, GridCellSelection> Moved;
        public event Action<Pawn> Died;
        public event Action<Pawn> Eats;
        
        public GridCell Cell => _cell;
        public int Distance => _distance;

        private MeshRenderer _mesh;
        private Color _notSelected;
        private Color _selected;
        private GridCell _cell;
        private int _direction;
        private int _distance;

        private void Awake()
        {
            _distance = 1;
        }

        private void OnMouseDown()
        {
            Selected?.Invoke(this);
        }

        public void Init(GridCell cell)
        {
            _cell = cell;
            cell.LinkPawn(this);
            
            Translate(cell.transform);
        }

        public void Select()
        {
            _mesh.material.color = _selected;
        }

        public void Move(GridCellSelection to)
        {
            var from = _cell;
            _cell.UnlinkPawn();
 
            _cell = to.Cell;
            to.Cell.PlacePawn(this);
            
            Translate(to.Cell.transform);

            Moved?.Invoke(this, from, to);
        }

        public void Eat(Pawn pawn)
        {
            pawn.Die();
            Eats?.Invoke(this);
        }
        
        public void Deselect()
        {
            _mesh.material.color = _notSelected;
        }

        public bool IsEnemy(Pawn other)
        {
            return !other.CompareTag(tag);
        }

        public void AssignPlayer(Player player)
        {
            _notSelected = player.Primary;
            _selected = player.Selected;
            _direction = player.Direction;

            gameObject.tag = player.tag;

            _mesh = GetComponent<MeshRenderer>();
            _mesh.material.color = _notSelected;
        }

        public List<Vector2Int> GetAroundAxes()
        {
            var direction = new Direction(_direction);
            return direction.Around();
        }

        public List<Vector2Int> GetForwardAxes() {
            var direction = new Direction(_direction);
            return direction.Forward();
        }
        
        private void Die()
        {
            _cell.UnlinkPawn();
            Died?.Invoke(this);

            Destroy(gameObject);
        }

        private void Translate(Transform other)
        {
            transform.SetParent(other);
            transform.position = new Vector3(
                other.position.x,
                1,
                other.position.z
            );
        }
    }
}

