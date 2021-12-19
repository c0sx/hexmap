using System.Collections.Generic;

using UnityEngine;

namespace Grid.Selector
{
    public class HexCellSelector : MonoBehaviour
    {
        public Selection.Group Select(HexGrid grid, Cell.GridCell cell)
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
                .ConvertAll<Cell.GridCell>(index => grid.Cells[index])
                .FindAll(cell => !cell.Occupied);

            otherCells.Add(cell);
            return new Selection.Group(cell, otherCells);
        }
    }

}

