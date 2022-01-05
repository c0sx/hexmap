using System.Collections.Generic;

using UnityEngine;

using Map.Cell;

namespace Map.Grid
{
    public class Cells : MonoBehaviour
    {
        [SerializeField] private GridCell _prefab;
        private List<GridCell> _cells;

        public List<GridCell> List => _cells;

        private void Awake()
        {
            _cells = new List<GridCell>();
        }

        public void Create(Metrics metrics) 
        {
            for (int z = 0, i = 0; z < metrics.Height; z++, i = 0) {
                for (int x = 0; x < metrics.Width; x++) {
                    var cell = CreateCell(metrics, i, x, z);
                    _cells.Add(cell);
                    i++;
                }
            }
        }

        public GridCell First()
        {
            return _cells[0];
        }

        public GridCell Last()
        {
            return _cells[_cells.Count - 1];
        }

        public GridCell IndexOf(int index) 
        {
            return _cells[index];
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

        public int GetMinX()
        {
            var min = _cells[0].Coordinates.X;
            for (var i = 1; i < _cells.Count - 1; ++i) {
                var x = _cells[i].Coordinates.X;
                if (x < min) {
                    min = x;
                }
            }

            return min;
        }

        public int GetMaxX()
        {
            var max = _cells[0].Coordinates.X;
            for (var i = 1; i < _cells.Count - 1; ++i) {
                var x = _cells[i].Coordinates.X;
                if (x > max) {
                    max = x;
                }
            }

            return max;
        }

        public int GetMinZ()
        {
            var min = _cells[0].Coordinates.Z;
            for (var i = 1; i < _cells.Count - 1; ++i) {
                var z = _cells[i].Coordinates.Z;
                if (z < min) {
                    min = z;
                }
            }

            return min;
        }

        public int GetMaxZ() 
        {
            var max = _cells[0].Coordinates.Z;
            for (var i = 1; i < _cells.Count - 1; ++i) {
                var z = _cells[i].Coordinates.Z;
                if (z > max) {
                    max = z;
                }
            }

            return max;
        }

        private GridCell CreateCell(Metrics metrics, int i, int x, int z)
        {
            var position = metrics.GetPositionFor(i, x, z);

            var cell = Instantiate<GridCell>(_prefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;

            var coordinates = Coordinates.FromOffsetCoordinates(x, z);
            cell.Init(coordinates, metrics);

            return cell;
        }
    }
}


