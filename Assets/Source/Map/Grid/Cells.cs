using System.Collections.Generic;

using UnityEngine;

using Map.Cell;

namespace Map.Grid
{
    public class Cells
    {
        private readonly List<GridCell> _cells;

        public Cells(List<GridCell> list)
        {
            _cells = list;
        }

        public GridCell First()
        {
            return _cells[0];
        }

        public GridCell Last()
        {
            return _cells[_cells.Count - 1];
        }

        public List<GridCell> GetNFirst(int size)
        {
            return _cells.GetRange(0, size);
        }

        public List<GridCell> GetNLast(int size)
        {
            var offset = _cells.Count - size;
            return _cells.GetRange(offset, size);
        }

        public GridCell FindByCoordinates(Coordinates coordinates)
        {
            return _cells.Find(one => one.Coordinates.IsEqual(coordinates));
        }
    }
}


