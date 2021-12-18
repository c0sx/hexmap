using System.Collections.Generic;

using UnityEngine;

namespace Grid.Cell
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class HexCellMesh : MonoBehaviour
    {
        private Mesh _mesh;
        private List<Vector3> _vertices;
        private List<int> _triangles;
        private MeshFilter _meshFilter;
        private MeshCollider _collider;
        private MeshRenderer _renderer;

        public Mesh Mesh => _mesh;
        public MeshFilter MeshFilter => _meshFilter;
        public MeshRenderer MeshRenderer => _renderer;

        private void Awake()
        {
            _mesh = new Mesh();
            _mesh.name = "Hex Cell Mesh";

            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = _mesh;

            _collider = GetComponent<MeshCollider>();
            _renderer = GetComponent<MeshRenderer>();

            _vertices = new List<Vector3>();
            _triangles = new List<int>();
        }

        public void Triangulate(HexMetrics metrics, HexCell cell)
        {
            _mesh.Clear();
            _vertices.Clear();
            _triangles.Clear();

            TriangulateCell(metrics, cell);

            _mesh.vertices = _vertices.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.RecalculateNormals();

            _collider.sharedMesh = _mesh;
        }

        public void Color(Color color)
        {
            _renderer.material.color = color;
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

}

