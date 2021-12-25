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
            if (turn.Tag == "Bottom Player") {
               return MoveToTop(grid, cell);
            }
            else if (turn.Tag == "Top Player") {
                return MoveToBottom(grid, cell);
            }

            throw new System.Exception("Player Tag not found");
        }

        private Group MoveToTop(HexGrid grid, GridCell cell)
        {
            var index = grid.Cells.FindIndex(one => one == cell);
            var width = grid.Width;
            var heigth = grid.Height;
            var rowIndex = Mathf.FloorToInt(index / width);
            var rowOffset = rowIndex % 2 == 1 ? 0 : -1;

            var leftOffset = index - rowIndex * width;

            var topRowIndex = rowIndex + 1;
            var topLeft = topRowIndex * width + leftOffset + rowOffset;
            var topRight = topLeft + 1;

            var isLeftBorder = index - 10 * rowIndex == 0;
            var isRightBorder = index - 9 - 10 * rowIndex == 0;

            var borders = new Borders(grid.Width, grid.Height);
            var indexes = borders.IncludesIndexes(topRowIndex, new List<int> { topLeft, topRight });
            var otherCells = indexes
                .ConvertAll<GridCell>(index => grid.Cells[index])
                .FindAll(cell => !cell.Occupied);

            otherCells.Add(cell);
            return new Group(cell, otherCells);
        }

        private Group MoveToBottom(HexGrid grid, GridCell cell)
        {
            var index = grid.Cells.FindIndex(one => one == cell);
            var width = grid.Width;
            var heigth = grid.Height;

            var rowIndex = Mathf.FloorToInt(index / width);
            var rowOffset = rowIndex % 2 == 1 ? 0 : -1;

            var leftOffset = index - rowIndex * width;

            var bottomRowIndex = rowIndex - 1;
            var bottomLeft = bottomRowIndex * width + leftOffset + rowOffset;
            var bottomRight = bottomLeft + 1;

            var borders = new Borders(grid.Width, grid.Height);
            var indexes = borders.IncludesIndexes(bottomRowIndex, new List<int> { bottomLeft, bottomRight });
            var otherCells = indexes
                .ConvertAll<GridCell>(index => grid.Cells[index])
                .FindAll(cell => !cell.Occupied);

            otherCells.Add(cell);
            return new Group(cell, otherCells);
        }
    }

}

