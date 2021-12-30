using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Unit;

namespace Map.Grid
{
    public class Spawners : MonoBehaviour
    {
        public Action<Pawn> Clicked;
        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;

        private List<Pawn> _pawns;

        private List<Pawn> Pawns => _pawns;

        private void Awake()
        {
            _pawns = new List<Pawn>();
        }

        public void Create(HexGrid grid)
        {
            var first = grid.FirstCell();
            var last = grid.LastCell();

            _bottom.Place(first);
            _top.Place(last);
        }

        public void Spawn(HexGrid grid)
        {
            var width = grid.Width;
            var bottomSlice = grid.GetNFirst(_bottom.Size * width);
            foreach (var cell in bottomSlice) {
                SpawnPawn(cell, _bottom);
            }

            var topSlice = grid.GetNLast(_top.Size * width);
            foreach (var cell in topSlice) {
                SpawnPawn(cell, _top);
            }
        }

        public void Subscribe()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked += OnClicked;
            }
        }

        public void Unsubscribe()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked -= OnClicked;
            }
        }

        private void SpawnPawn(GridCell cell, Spawner spawner)
        {
            var pawn = spawner.Spawn();
            _pawns.Add(pawn);

            cell.PlacePawn(pawn);
        }

        private void OnClicked(Pawn pawn)
        {
            Clicked?.Invoke(pawn);
        }
    }
}
