using UnityEngine;

[RequireComponent(typeof(HexCellMesh))]
public class HexCell : MonoBehaviour
{
    [SerializeField] private HexCoordinates _coordinates;
    [SerializeField] private Pawn _pawnPrefab;
    private HexCellMesh _mesh;
    private HexMetrics _metrics;
    private Pawn _pawn;

    public void Init(HexCoordinates coordinates, HexMetrics metrics)
    {
        _coordinates = coordinates;
        _metrics = metrics;

        _mesh = GetComponent<HexCellMesh>();
    }

    public void Triangulate()
    {
        _mesh.Triangulate(_metrics, this);
    }

    public void PlacePawn(Pawn pawn)
    {
        _pawn = pawn;

        _pawn.transform.SetParent(transform);
        _pawn.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    public override string ToString()
    {
        return _coordinates.ToString();
    }
}
