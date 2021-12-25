using System.Collections.Generic;

using UnityEngine;

using Map.Grid;
using Map.Selection;
using Map.Cell;

namespace Map.Selector
{
    public class HexCellSelector : MonoBehaviour
    {
        public Group Select(Turn turn, HexGrid grid, GridCell cell)
        {
            var axises = cell.Pawn.GetAxises();
            var borders = new Borders(grid);
            var width = grid.Width;
            var heigth = grid.Height;

            var current = cell.Coordinates;
            var vector = current.ToVector2Int();
            var coordinates = new List<Coordinates>() { current };
            foreach (var axis in axises) {
                var coordinate = vector + axis;
                coordinates.Add(Coordinates.FromVector2(coordinate));
            }

            var otherCells = borders
                .IncludesCoordinates(coordinates)
                .ConvertAll(coordinate => grid.Cells.FindByCoordinates(coordinate));

            return new Group(cell, otherCells);
        }
    }

}

