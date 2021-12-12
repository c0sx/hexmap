using UnityEngine;

using Grid;

public class MovementHighlighter : MonoBehaviour
{
    [SerializeField] private HexGrid _grid;

    public void Highlight(HexCell cell) 
    {
        var index = _grid.Cells.FindIndex(one => one == cell);
        var width = _grid.Width;
        var heigth = _grid.Height;
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

        // var row = Mathf.FloorToInt(index / width);
        // Debug.Log("row " + row);

        // var topRowIndex = row + 1;
        // Debug.Log("topRowIndex " + topRowIndex);

        // var bottomRowIndex = row - 1;
        // Debug.Log("bottomRowIndex " + bottomRowIndex);

        // var topLeft = topRowIndex * width + index - 1;
        // Debug.Log("temp " + topRowIndex * width);
        // Debug.Log("topLeft " + topLeft);

        // var topRight = topRowIndex * width + index;
        // var bottomLeft = bottomRowIndex * width + index - 1;
        // var bottomRight = bottomRowIndex * width + index;

        var around = new int[] { topLeft, topRight, left, right, bottomLeft, bottomRight };
        foreach (var i in around) {
            _grid.Cells[i].Highlight();
        }
    }
}
