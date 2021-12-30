using System.Collections.Generic;

using UnityEngine;

using Map.Grid;
using Map.Selection;
using Map.Cell;
using Rules;

namespace Map.Selector
{
    public class GridCellSelector : MonoBehaviour
    {
        public Group Select(Turn turn, HexGrid grid, GridCell cell)
        {
            var axises = cell.Pawn.GetAxises();
            var borders = new Borders(grid);
            var width = grid.Width;
            var heigth = grid.Height;

            var current = cell.Coordinates;
            var vector = current.ToVector2Int();
            var cells = new List<SelectedContainer>();

            foreach (var axis in axises) {
                var next = vector + axis;
                var list = Expand(grid, cell, next, axis);
                cells.AddRange(list);
            }

            var otherCells = borders.Includes(cells);
            return new Group(cell, otherCells);
        }

        private List<SelectedContainer> Expand(HexGrid grid, GridCell cell, Vector2Int point, Vector2Int axis)
        {
            var target = Coordinates.FromVector2(point);
            var targetCell = grid.FindByCoordinates(target);

            var list = new List<SelectedContainer>();
            if (!targetCell.Occupied) {
                list.Add(new SelectedContainer(targetCell, axis));
                
                return list;
            }

            // if cell is occupied
            // if its team-mate pawn - ignore this coordinate
            // if its enemy 
            // check next cell after enemy
            // if its empty - add empty cell
            // else ignore

            var pawn = targetCell.Pawn;
            if (cell.Pawn.IsEnemy(pawn)) {
                var next = point + axis;
                var target2 = Coordinates.FromVector2(next);
                var targetCell2 = grid.FindByCoordinates(target2);

                if (!targetCell2.Occupied) {
                    list.Add(new SelectedContainer(targetCell2, axis));
                }
            }

            return list;
        }
    }

}

