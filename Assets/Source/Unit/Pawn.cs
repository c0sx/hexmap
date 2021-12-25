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

        public GridCell Cell => _cell;

        private void OnMouseDown()
        {
            Clicked?.Invoke(this);
        }

        public void Select()
        {
            _mesh.material.color = _selected;
        }

        public void Deselect()
        {
            _mesh.material.color = _notSelected;
        }

        public void AssignTeam(Team team)
        {
            _notSelected = team.Primary;
            _selected = team.Selected;
            _direction = team.Direction;

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

