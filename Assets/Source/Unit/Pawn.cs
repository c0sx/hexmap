using System;
using System.Collections.Generic;

using UnityEngine;

using Hexmap.Map.Cell;
using Hexmap.Map.Movement.Selector;

namespace Hexmap.Unit
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Pawn : MonoBehaviour
    {
        public event Action<Pawn> Selected;
        public event Action<Pawn, GridCell, GridCellSelection> Moved;
        public event Action<Pawn> Died;
        public event Action<Pawn> Eats;

        private MeshRenderer _mesh;
        private Color _notSelected;
        private Color _selected;
        private GridCell _cell;
        private int _direction;
        private int _distance = 1;

        public GridCell Cell => _cell;
        public int Distance => _distance;

        private void OnMouseDown()
        {
            Selected?.Invoke(this);
        }

        public void Select()
        {
            _mesh.material.color = _selected;
        }

        public void PlaceTo(GridCell cell)
        {
            _cell = cell;
            cell.LinkPawn(this);

            transform.SetParent(cell.transform);
            transform.position = new Vector3(
                cell.transform.position.x, 
                1, 
                cell.transform.position.z
            );
        }

        public void Move(GridCellSelection to)
        {
            var from = _cell;
            _cell.UnlinkPawn();
 
            PlaceTo(to.Cell);

            Moved?.Invoke(this, from, to);
        }

        public void Eat(Pawn pawn)
        {
            pawn.Die();
            Eats?.Invoke(this);
        }

        public void Die()
        {
            _cell.UnlinkPawn();
            Died?.Invoke(this);

            Destroy(gameObject);
        }

        public void Deselect()
        {
            _mesh.material.color = _notSelected;
        }

        public bool IsEnemy(Pawn other)
        {
            return tag != other.tag;
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

        public List<Vector2Int> GetAroundAxises()
        {
            var direction = new Direction(_direction);
            return direction.Around();
        }

        public List<Vector2Int> GetForwardAxises() {
            var direction = new Direction(_direction);
            return direction.Forward();
        }
    }
}

