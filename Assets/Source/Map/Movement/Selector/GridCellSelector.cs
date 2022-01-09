using System.Collections.Generic;

using UnityEngine;

using Map.Grid;
using Map.Cell;
using Unit;

namespace Map.Movement.Selector
{
    public class GridCellSelector : MonoBehaviour
    {
        public List<GridCellSelection> SelectAvailableVectors(HexGrid grid, Pawn pawn)
        {
            // check every vector for eating available;
            var eatingAxes = GetEatingVectors(grid, pawn);
            var axes = eatingAxes.Count > 0 ? eatingAxes : pawn.GetForwardAxises();
            var selections = new List<GridCellSelection>();

            foreach (var axis in axes) {
                // for every axis generate vector with D distance;
                var vector = CreateVectorForAxis(grid, pawn, axis);
                selections.Add(
                    new GridCellSelection(pawn.Cell, axis, vector)
                );
            }

            return selections;
        }

        public List<Vector2Int> GetEatingVectors(HexGrid grid, Pawn pawn)
        {
            var eatingVectors = new List<Vector2Int>();
            var axes = pawn.GetAroundAxises();

            foreach (var axis in axes) {
                var next = pawn.Cell.Coordinates.ToVector2Int() + axis;
                var cell = grid.FindByVector2(next);
                if (cell == null || !cell.Occupied || !pawn.IsEnemy(cell.Pawn)) {
                    continue;
                }

                var backward = next + axis;
                var backwardCell = grid.FindByVector2(backward);
                if (cell.Occupied && backwardCell != null && !backwardCell.Occupied) {
                    eatingVectors.Add(axis);
                }
            }

            return eatingVectors;
        }

        private List<GridCell> CreateVectorForAxis(HexGrid grid, Pawn pawn, Vector2Int axis)
        {
            // for every axis create vector with D distance
            var vector = new List<GridCell>();
            for (var i = 0; i < pawn.Distance; ++i) {
                var next = pawn.Cell.Coordinates.ToVector2Int() + axis;
                var cell = grid.FindByVector2(next);
                if (cell != null) {
                    vector.Add(cell);
                }
            }

            var expanded = ExpandVector(grid, pawn, vector, axis);
            return expanded;
        }

        private List<GridCell> ExpandVector(HexGrid grid, Pawn pawn, List<GridCell> vector, Vector2Int axis)
        {
            if (vector.Count == 0) {
                return vector;
            }

            var last = vector[vector.Count - 1];
            if (last != null && last.Occupied && pawn.IsEnemy(last.Pawn)) {
                var next = last.Coordinates.ToVector2Int() + axis;
                var cell = grid.FindByVector2(next);
                if (cell != null) {
                    vector.Add(cell);
                }
            }

            return vector;
        }
    }
}

