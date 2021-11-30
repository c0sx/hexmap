using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class HexCellMesh : MonoBehaviour
{
    private Mesh _mesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;
    private MeshFilter _meshFilter;
    private MeshCollider _collider;
    private List<Color> _colors;

    public Action<Vector3> Clicked;

    private void Awake()
    {
        _mesh = new Mesh();
        _mesh.name = "Hex Cell Mesh";

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

        _collider = GetComponent<MeshCollider>();

        _vertices = new List<Vector3>();
        _triangles = new List<int>();
        _colors = new List<Color>();
    }

    private void OnMouseDown()
    {
        var inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            Clicked?.Invoke(hit.point);
        }
    }

    public void Triangulate(HexMetrics metrics, HexCell cell)
    {
        _mesh.Clear();
        _vertices.Clear();
        _triangles.Clear();
        _colors.Clear();

        TriangulateCell(metrics, cell);

        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();
        _mesh.colors = _colors.ToArray();

        _collider.sharedMesh = _mesh;
    }

    private void TriangulateCell(HexMetrics metrics, HexCell cell)
    {
        var center = Vector3.zero;
        var corners = metrics.Corners;

        for (var i = 0; i < 6; ++i) {
           AddTriangle(
                center,
                center + corners[i],
                center + corners[i + 1]
            );

            AddColor(cell.Color);
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

    private void AddColor(Color color) 
    {
        _colors.Add(color);
        _colors.Add(color);
        _colors.Add(color);
    }
}
