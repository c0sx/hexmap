using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Unit;

namespace Map.Grid
{
    public class Spawners : MonoBehaviour
    {
        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;

        public List<Pawn> GetPawns()
        {
            var topPawns = _top.Pawns;
            var bottomPawns = _bottom.Pawns;

            var list = new List<Pawn>(topPawns);
            list.AddRange(bottomPawns);
            return list;
        }

        public void Create(Cells cells)
        {
            var first = cells.First();
            var last = cells.Last();

            _bottom.Place(first);
            _top.Place(last);
        }

        public List<Pawn> Spawn(HexGrid grid)
        {
            var pawns = new List<Pawn>();
            var width = grid.Width;
            var bottomSlice = grid.GetNFirst(_bottom.Size * width);
            var bottom = FromSpawner(bottomSlice, _bottom);
            pawns.AddRange(bottom);

            var topSlice = grid.GetNLast(_top.Size * width);
            var top = FromSpawner(topSlice, _top);
            pawns.AddRange(top);

            return pawns;
        }

        private List<Pawn> FromSpawner(List<GridCell> cells, Spawner spawner) 
        {
            var pawns = new List<Pawn>();
            foreach (var cell in cells) {
                var pawn = spawner.SpawnPawn();
                pawn.Init(cell);
                pawns.Add(pawn);
            }

            return pawns;
        }
    }
}
