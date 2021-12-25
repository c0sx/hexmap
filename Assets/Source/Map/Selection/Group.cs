using System.Collections.Generic;

using Map.Cell;

namespace Map.Selection
{
    public class Group
    {
        private readonly GridCell _center;
        private readonly List<GridCell> _cells;

        public GridCell Center => _center;
        public List<GridCell> Cells => _cells;

        public Group(GridCell center, List<GridCell> cells)
        {
            _center = center;
            _cells = cells;
        }
    }
}

