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

        public void Spawn(HexGrid grid)
        {
            var width = grid.Width;
            var bottomSlice = grid.GetNFirst(_bottom.Size * width);
            FromSpawner(bottomSlice, _bottom);

            var topSlice = grid.GetNLast(_top.Size * width);
            FromSpawner(topSlice, _top);
        }

        public Pawn SpawnQueen(GridCell cell)
        {
            var pawn = cell.Pawn;
            var list = new List<Spawner> {_top, _bottom};
            var spawner = list.Find(spawner => spawner.Contains(pawn));

            var queen = spawner.SpawnQueen(pawn);
            queen.Init(pawn.Cell, cell.Direction);
            
            Destroy(pawn.gameObject);
            return queen;
        }

        private void FromSpawner(List<GridCell> cells, Spawner spawner) 
        {
            foreach (var cell in cells) {
                var pawn = spawner.SpawnPawn();
                pawn.Init(cell, spawner.Player.Direction);
            }
        }
    }
}
