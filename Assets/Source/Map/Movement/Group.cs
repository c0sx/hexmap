using System.Collections.Generic;

using Hexmap.Map.Cell;
using Hexmap.Map.Movement.Selector;

namespace Hexmap.Map.Movement
{
    public class Group
    {
        private readonly GridCell _center;
        private readonly List<GridCellSelection> _cells;

        public GridCell Center => _center;
        public List<GridCellSelection> Cells => _cells;

        public Group(GridCell center, List<GridCellSelection> cells)
        {
            _center = center;
            _cells = cells;
        }
    }
}

