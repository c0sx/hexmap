using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private HexCoordinates _coordinates;
    [SerializeField] private HexCellMesh _mesh;
    private Color _color;

    public Color Color => _color;

    public void Init(HexCoordinates coordinates, Color color)
    {
        _coordinates = coordinates;
        _color = color;
    }

    public void Triangulate(HexMetrics metrics)
    {
        // _mesh.Triangulate(metrics, this);
    }

    public void Touch(Color color)
    {
        _color = color;
    }

    public override string ToString()
    {
        return _coordinates.ToString();
    }
}
