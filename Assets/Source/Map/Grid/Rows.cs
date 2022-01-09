using System.Collections.Generic;

using UnityEngine;

using Map.Cell;

namespace Map.Grid
{
    public class Rows : MonoBehaviour
    {
        [SerializeField] private GridCell _prefab;

        public List<GridCell> CreateCell(Options options)
        {
            var metrics = new Metrics(options);
            
            var list = new List<GridCell>();
            for (var z = 0; z < metrics.Height; ++z)
            {
                for (var x = 0; x < metrics.Width; ++x)
                {
                    var cell = CreateCell(metrics, x, z);
                    list.Add(cell);
                }
            }

            return list;
        }

        private GridCell CreateCell(Metrics metrics, int x, int z)
        {
            var cell = Instantiate(_prefab, transform, false);
            cell.Init(metrics, x, z);

            return cell;
        }
    }
}