using System;

using UnityEngine;

using Map.Cell;

namespace Map.Movement.Selector
{
    public class SelectedContainer
    {
        public Action<GridCell, Vector2Int> Clicked;

        private readonly GridCell _cell;
        private readonly Vector2Int _axis;

        public GridCell Cell => _cell;
        public Vector2Int Axis => _axis;

        public SelectedContainer(GridCell cell, Vector2Int axis)
        {
            _cell = cell;
            _axis = axis;

            _cell.Clicked += OnClicked;
        }

        private void OnClicked(GridCell to)
        {
            Clicked?.Invoke(to, _axis);
        }
    }
}


