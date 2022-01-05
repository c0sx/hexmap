using System.Collections.Generic;

using Map.Cell;
using Map.Movement.Selector;

namespace Map.Movement
{
    public class Group
    {
        private readonly GridCell _center;
        private readonly List<SelectedContainer> _cells;

        public GridCell Center => _center;
        public List<SelectedContainer> Cells => _cells;

        public Group(GridCell center, List<SelectedContainer> cells)
        {
            _center = center;
            _cells = cells;
        }
    }
}

