using System.Collections.Generic;

using Map.Grid;
using Map.Cell;

namespace Map.Selector
{
    public class Borders
    {
        
        private HexGrid _grid;

        public Borders(HexGrid grid)
        {
            _grid = grid;
        }

        public List<GridCell> Includes(List<GridCell> cells) 
        {
            return cells.FindAll(cell => IncludesOne(cell));
        }

        private bool IncludesOne(GridCell cell)
        {
            var coordinate = cell.Coordinates;
            var maxX = _grid.Cells.GetMaxX();
            var minX = _grid.Cells.GetMinX();
            var maxZ = _grid.Cells.GetMaxZ();
            var minZ = _grid.Cells.GetMinZ();

            return coordinate.X >= minX && coordinate.X <= maxX && coordinate.Z >= minZ && coordinate.Z <= maxZ;
        }
    }
}

