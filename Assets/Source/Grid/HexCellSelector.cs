using System.Collections.Generic;

using UnityEngine;

namespace Grid
{
    public class HexCellSelector : MonoBehaviour
    {
        public List<HexCell> Select(HexGrid grid, HexCell cell)
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

            var left = index - 1;
            var right = index + 1;

            var bottomRowIndex = rowIndex - 1;
            var bottomLeft = bottomRowIndex * width + leftOffset + rowOffset;
            var bottomRight = bottomLeft + 1;

            var indexes = new List<int>() { topLeft, topRight, left, right, bottomLeft, bottomRight };
            var otherCells = indexes.ConvertAll<HexCell>(index => {
                return grid.Cells[index];
            });

            otherCells.Add(cell);
            return otherCells;
        }
    }

}

