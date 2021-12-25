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

        public List<Coordinates> IncludesCoordinates(List<Coordinates> coordinates) 
        {
            return coordinates.FindAll(coordinate => Includes(coordinate));
        }

        private bool Includes(Coordinates coordinate)
        {
            var maxX = _grid.Cells.GetMaxX();
            var minX = _grid.Cells.GetMinX();
            var maxZ = _grid.Cells.GetMaxZ();
            var minZ = _grid.Cells.GetMinZ();

            return coordinate.X >= minX && coordinate.X <= maxX && coordinate.Z >= minZ && coordinate.Z <= maxZ;
        }
    }
}

