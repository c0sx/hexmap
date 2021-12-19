using UnityEngine;

using System;

using Grid.Cell;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Pawn : MonoBehaviour
    {
        private MeshRenderer _mesh;
        private Color _notSelected;
        private Color _selected;
        private GridCell _cell;

        public Action<Pawn> Clicked;

        public GridCell Cell => _cell;

        private void OnMouseDown()
        {
            ToggleSelection();
        }

        public void Init(Color defaultColor, Color selectedColor)
        {
            _notSelected = defaultColor;
            _selected = selectedColor;

            _mesh = GetComponent<MeshRenderer>();
            _mesh.material.color = _notSelected;
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

        }

        private void ToggleSelection()
        {
            Clicked?.Invoke(this);
        }
    }
}

