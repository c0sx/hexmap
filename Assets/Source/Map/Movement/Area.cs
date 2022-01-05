using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Map.Grid;
using Map.Movement.Selector;
using Unit;

namespace Map.Movement
{
    [RequireComponent(typeof(GridCellSelector))]
    public class Area : MonoBehaviour
    {
        private Group _current;

        public Action<Pawn> PawnMoved;
        public Action<Pawn> PawnEaten;

        [SerializeField] private HexGrid _grid;

        private GridCellSelector _selector;

        private void Awake()
        {
            _selector = GetComponent<GridCellSelector>();
        }

        public void Select(Pawn pawn)
        {
            // deselect previous cells
            ClearSelection();

            // calculate selection zone
            _current = _selector.Select(_grid, pawn.Cell);
            
            // select cells from group
            _current.Cells.ForEach(selection => selection.Cell.Select());

            // turn cell state to highlighted instead of this subscription
            Subscribe(_current);
        }

        private void ClearSelection()
        {
            if (_current == null) {
                return;
            }

            Unsubscribe(_current);
            foreach (var cell in _current.Cells) {
                cell.Cell.Deselect();
            }
        }

        private void Subscribe(Group group)
        {
            var center = group.Center;
            var other = group.Cells.FindAll(cell => cell.Cell != center);

            foreach (var cell in other) {
                cell.Clicked += MovePawn;
            }
        }

        private void Unsubscribe(Group group)
        {
            var center = group.Center;
            var other = group.Cells.FindAll(cell => cell.Cell != center);

            foreach (var cell in other) {
                cell.Clicked -= MovePawn;
            }
        }

        private void MovePawn(GridCell to, Vector2Int axis)
        {
            var pawn = _current.Center.Pawn;
            pawn.Move(to);
            pawn.Deselect();
            PawnMoved?.Invoke(pawn);

            var end = _current.Center.Coordinates;
            var start = to.Coordinates;

            Debug.Log("start " + start.ToVector2Int() + " axis " + axis + " end " + end.ToVector2Int());

            if (Eat(start, end, axis)) {
                // PawnEaten?.Invoke(_current.Center.Pawn);
            }

            ClearSelection();
        }

        private bool Eat(Coordinates start, Coordinates end, Vector2Int axis)
        {
            var enemies = new List<GridCell>();
            var from = start.ToVector2Int();
            var to = end.ToVector2Int();

            while (from != to) {
                from -= axis;
                Debug.Log("from " + from);
                var cell = _grid.FindByCoordinates(Coordinates.FromVector2(from));
                if (cell.Occupied) {
                    enemies.Add(cell);
                }
            }

            foreach (var cell in enemies) {
                Debug.Log("DIE");
                cell.Pawn.Die();
            }

            return enemies.Count > 0;
        }
    }

}
