using System.Collections.Generic;

using Grid.Cell;

namespace Grid.Selection
{
    public class Group
    {
        private readonly HexCell _center;
        private readonly List<HexCell> _cells;

        public HexCell Center => _center;
        public List<HexCell> Cells => _cells;

        public Group(HexCell center, List<HexCell> cells)
        {
            _center = center;
            _cells = cells;
        }
    }
}

