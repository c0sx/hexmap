using System.Collections.Generic;

using UnityEngine;

using Map.Cell;

public class GridCellSelection
{
    private GridCell _cell;
    private readonly Vector2Int _axis;
    private List<GridCell> _vector;

    // TODO: work through vector (for queens)
    public GridCell Cell => _cell;
    public Vector2Int Axis => _axis;
    public List<GridCell> Vector => _vector;

    public GridCellSelection(GridCell cell, Vector2Int axis, List<GridCell> vector)
    {
        _cell = cell;
        _axis = axis;
        _vector = vector;
    }

    public void Select(GridCell cell)
    {
        var index = _vector.IndexOf(cell);
        if (index == -1) {
            throw new System.Exception("Selection vector doesn't contains cell");
        }

        _cell = _vector[index];
    }

    public bool Contains(GridCell cell) 
    {
        return _vector.Find(one => one == cell);
    }
}
