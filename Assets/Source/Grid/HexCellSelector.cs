using System.Collections.Generic;

using UnityEngine;

namespace Grid
{
    public class HexCellSelector : MonoBehaviour
    {
        public Selection.Group Select(HexGrid grid, Cell.HexCell cell)
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

            var isTopLeftAvailable = isLeftBorder ? topLeft - 10 == index : true;
            var isTopRightAvailable = isRightBorder ? topRight - 10 == index : true;

            var indexes = new List<int>();
            if (isTopLeftAvailable) {
                indexes.Add(topLeft);
            }

            if (isTopRightAvailable) {
                indexes.Add(topRight);
            }
                
            var otherCells = indexes.ConvertAll<Cell.HexCell>(index => {
                return grid.Cells[index];
            });

            otherCells.Add(cell);
            return new Selection.Group(cell, otherCells);
        }
    }

}

