using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    private Mesh _mesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;
    private MeshFilter _meshFilter;

    private void Awake() 
    {
        _mesh = new Mesh();
        _mesh.name = "Hex Mesh";

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

        _vertices = new List<Vector3>();
        _triangles = new List<int>();
    }

    public void Triangulate(HexMetrics metrics, HexCell[] cells)
    {
        _mesh.Clear();
        _vertices.Clear();
        _triangles.Clear();

        foreach (var cell in cells) {
            TriangulateCell(metrics, cell);
        }

        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();
    }

    private void TriangulateCell(HexMetrics metrics, HexCell cell)
    {
        var center = cell.transform.localPosition;
        var corners = metrics.GetCorners();

        for (var i = 0; i < 6; ++i) {
           AddTriangle(
                center,
                center + corners[i],
                center + corners[i + 1]
            );
        }
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var index = _vertices.Count;
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);

        _triangles.Add(index);
        _triangles.Add(index + 1);
        _triangles.Add(index + 2);
    }
}
