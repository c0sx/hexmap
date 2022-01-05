using System.Collections.Generic;

using Map.Grid;

namespace Map.Movement.Selector
{
    public class Borders
    {
        
        private HexGrid _grid;

        public Borders(HexGrid grid)
        {
            _grid = grid;
        }

        public List<SelectedContainer> Includes(List<SelectedContainer> cells) 
        {
            return cells.FindAll(cell => IncludesOne(cell));
        }

        private bool IncludesOne(SelectedContainer cell)
        {
            var coordinate = cell.Cell.Coordinates;
            var maxX = _grid.GetMaxX();
            var minX = _grid.GetMinX();
            var maxZ = _grid.GetMaxZ();
            var minZ = _grid.GetMinZ();

            return coordinate.X >= minX && coordinate.X <= maxX && coordinate.Z >= minZ && coordinate.Z <= maxZ;
        }
    }
}

