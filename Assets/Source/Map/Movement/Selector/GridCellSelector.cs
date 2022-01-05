using System.Collections.Generic;

using UnityEngine;

using Map.Grid;
using Map.Cell;
using Unit;

namespace Map.Movement.Selector
{
    public class GridCellSelector : MonoBehaviour
    {
        // public List<GridCellSelection> SelectForward(HexGrid grid, Pawn pawn)
        // {
        //     var axises = pawn.GetForwardAxises();
        //     var cell = pawn.Cell;
        //     var coordinates = cell.Coordinates;
        //     var vector = coordinates.ToVector2Int();

        //     var selections = new List<GridCellSelection>(); 

        //     // 2 forward vectors for each forward axis;
        //     foreach (var axis in axises) {
        //         var point = vector + axis;
        //         var pointCell = grid.FindByVector2(point);
        //         var selection = new GridCellSelection(pointCell, axis);
                
        //         var expanded = Expand(grid, cell, point, axis);
        //         selection.Expand(expanded);

        //         selections.Add(selection);
        //     }

        //     var borders = new Borders(grid);
        //     var included = borders.Includes(selections);
        //     return included;
        // }

        public List<GridCellSelection> SelectAvailableVectors(HexGrid grid, Pawn pawn)
        {
            // check every vector for eating available;
            var eatingAxises = GetEatingVectors(grid, pawn);
            var axises = eatingAxises.Count > 0 ? eatingAxises : pawn.GetForwardAxises();
            var selections = new List<GridCellSelection>();

            foreach (var axis in axises) {
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
            var axises = pawn.GetAroundAxises();

            foreach (var axis in axises) {
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

        // public List<GridCellSelection> SelectAroundCells(HexGrid grid, Pawn pawn)
        // {
        //     var axises = pawn.GetAroundAxises();

        //     var coordinates = pawn.Cell.Coordinates;
        //     var vector = coordinates.ToVector2Int();

        //     var cells = new List<GridCellSelection>();
        //     var selections = new List<GridCellSelection>();

        //     foreach (var axis in axises) {
        //         var point = vector + axis;
        //         var pointCoordinates = Coordinates.FromVector2(point);
        //         var pointCell = grid.FindByCoordinates(pointCoordinates);
        //         var selection = new GridCellSelection(pointCell, axis);

        //         var expanded = Expand(grid, pawn.Cell, point, axis);
        //         selection.Expand(expanded);

        //         selections.Add(selection);
        //     }

        //     var borders = new Borders(grid);
        //     var included = borders.Includes(cells);
        //     return included;
        // }

        // private List<GridCell> Expand(HexGrid grid, GridCell cell, Vector2Int point, Vector2Int axis)
        // {
        //     var targetCell = grid.FindByVector2(point);
        //     if (targetCell == null || !targetCell.Occupied) {
        //         return new List<GridCell> { cell };
        //     }

        //     // if cell is occupied
        //     // if its team-mate pawn - ignore this coordinate
        //     // if its enemy 
        //     // check next cell after enemy
        //     // if its empty - add empty cell
        //     // else ignore

        //     var list = new List<GridCell>();
        //     var pawn = targetCell.Pawn;
        //     if (cell.Pawn.IsEnemy(pawn)) {
        //         var next = point + axis;
        //         var enemyCell = grid.FindByVector2(next);

        //         if (enemyCell != null && !enemyCell.Occupied) {
        //             list.Add(enemyCell);
        //         }
        //     }

        //     return list;
        // }
    }

}

