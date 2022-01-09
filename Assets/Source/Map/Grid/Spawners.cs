using System;
using System.Collections.Generic;

using UnityEngine;

using Hexmap.Map.Cell;
using Hexmap.Unit;

namespace Hexmap.Map.Grid
{
    public class Spawners : MonoBehaviour
    {
        public Action<Pawn> Clicked;
        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;

        public void Create(HexGrid grid)
        {
            var first = grid.FirstCell();
            var last = grid.LastCell();

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
                var pawn = spawner.Spawn();
                pawn.PlaceTo(cell);
                pawns.Add(pawn);
            }

            return pawns;
        }
    }
}
