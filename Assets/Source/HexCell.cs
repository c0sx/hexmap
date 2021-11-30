using System;

using UnityEngine;

[RequireComponent(typeof(HexCellMesh))]
public class HexCell : MonoBehaviour
{
    [SerializeField] private HexCoordinates _coordinates;
    [SerializeField] private Color _current;
    [SerializeField] private Color _secondary;
    private HexCellMesh _mesh;
    private HexMetrics _metrics;

    public Color Color => _current;

    public void Init(HexCoordinates coordinates, HexMetrics metrics)
    {
        _coordinates = coordinates;
        _metrics = metrics;

        _mesh = GetComponent<HexCellMesh>();

        Subscribe();
    }

    public void Triangulate()
    {
        _mesh.Triangulate(_metrics, this);
    }

    public override string ToString()
    {
        return _coordinates.ToString();
    }

    private void Subscribe()
    {
        _mesh.Clicked += TouchCell;
    }

    private void TouchCell(Vector3 position)
    {
        var color = _current;
        _current = _secondary;
        _secondary = color;

        Triangulate();
    }
}
