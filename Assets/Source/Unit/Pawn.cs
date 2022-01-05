using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Cell;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Pawn : MonoBehaviour
    {
        private MeshRenderer _mesh;
        private Color _notSelected;
        private Color _selected;
        private GridCell _cell;
        private int _direction;

        public Action<Pawn> Clicked;
        public Action<Pawn> Died;

        public GridCell Cell => _cell;

        private void OnMouseDown()
        {
            Clicked?.Invoke(this);
        }

        public void Select()
        {
            _mesh.material.color = _selected;
        }

        public void Place(GridCell cell)
        {
            _cell = cell;

            transform.SetParent(cell.transform);
            transform.position = new Vector3(
                cell.transform.position.x, 
                1, 
                cell.transform.position.z
            );
        }

        public void Move(GridCell to)
        {
            _cell.RemovePawn();
            to.PlacePawn(this);
        }

        public void Die()
        {
            _cell.RemovePawn();
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

        public List<Vector2Int> GetAxises() {
            var left = new Vector2Int(1, -1) * _direction;
            var right = new Vector2Int(0, -1) * _direction;

            return new List<Vector2Int>() { left, right };
        }
    }
}

