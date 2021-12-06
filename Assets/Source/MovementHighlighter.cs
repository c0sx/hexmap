using UnityEngine;

public class MovementHighlighter : MonoBehaviour
{
    [SerializeField] private HexGrid _grid;

    public void Highlight(HexCell cell) 
    {
        var index = _grid.Cells.FindIndex(one => one == cell);
        var left = index - 1;
        var right = index + 1;

        var width = _grid.Width;
        var heigth = _grid.Height;

        var row = Mathf.FloorToInt(index / width);
        var topRowIndex = row + 1;
        var bottomRowIndex = row - 1;

        var topLeft = topRowIndex * width + index - 1;
        var topRight = topRowIndex * width + index;
        var bottomLeft = bottomRowIndex * width + index - 1;
        var bottomRight = bottomRowIndex * width + index;

        var around = new int[6] { left, right, topLeft, topRight, bottomLeft, bottomRight };
        foreach (var i in around) {
            _grid.Cells[i].Highlight();
        }
    }
}
