using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Map.Grid;

namespace Map.Selection
{
    public class Area : MonoBehaviour
    {
        private Group _current;

        public Action PawnMoved;
        public Action TurnEnded;

        [SerializeField] private HexGrid _grid;

        public void Select(Group group)
        {
            Clear();

            _current = group;

            transform.position = new Vector3(
                0,
                0.2f,
                0
            );

            group.Center.SelectPawn();
            group.Cells.ForEach(cell => cell.Cell.Select());
            Subscribe(group);
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
            _current.Center.Pawn.Move(to);

            var start = _current.Center.Coordinates;
            var end = to.Coordinates;

            Eat(start, end, axis);
            TurnEnded?.Invoke();    
            Clear();
        }

        private void Clear()
        {
            if (_current != null) {
                Unsubscribe(_current);
            }

            _current?.Cells.ForEach(cell => cell.Cell.Deselect());
        }

        private bool Eat(Coordinates start, Coordinates end, Vector2Int axis)
        {
            var enemies = new List<GridCell>();
            var from = start.ToVector2Int();
            var to = end.ToVector2Int();

            while (from != to) {
                from += axis;
                var cell = _grid.FindByCoordinates(Coordinates.FromVector2(from));
                if (cell.Occupied) {
                    enemies.Add(cell);
                }
            }

            foreach (var cell in enemies) {
                cell.Pawn.Die();
            }

            return enemies.Count > 0;
        }
    }

}
