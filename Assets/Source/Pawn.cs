using UnityEngine;

using System;

using Grid;

[RequireComponent(typeof(MeshRenderer))]
public class Pawn : MonoBehaviour
{
    private MeshRenderer _mesh;
    private Color _current;
    private Color _previous;
    private HexCell _cell;

    public Action<Pawn> Clicked;

    public HexCell Cell => _cell;

    private void OnMouseDown()
    {
        ToggleSelection();
    }

    public void Init(Color color, Color previous)
    {
        _current = color;
        _previous = previous;

        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = color;
    }

    public void PlaceTo(HexCell cell)
    {
        _cell = cell;
        _cell.PlacePawn(this);
    }

    private void ToggleSelection()
    {
        Clicked?.Invoke(this);
    }
}
