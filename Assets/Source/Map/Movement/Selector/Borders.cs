using System.Collections.Generic;

using Hexmap.Map.Grid;
using Hexmap.Map.Cell;

namespace Hexmap.Map.Movement.Selector
{
    public class Borders
    {
        
        private HexGrid _grid;

        public Borders(HexGrid grid)
        {
            _grid = grid;
        }

        public List<GridCellSelection> Includes(List<GridCellSelection> selection) 
        {
            return selection.FindAll(selection => IncludesOne(selection));
        }

        private bool IncludesOne(GridCellSelection selection)
        {
            var last = selection.Vector[selection.Vector.Count - 1];
            var coordinate = last.Coordinates;
            var maxX = _grid.GetMaxX();
            var minX = _grid.GetMinX();
            var maxZ = _grid.GetMaxZ();
            var minZ = _grid.GetMinZ();

            return coordinate.X >= minX && coordinate.X <= maxX && coordinate.Z >= minZ && coordinate.Z <= maxZ;
        }
    }
}

