using System.Collections.Generic;

using UnityEngine;

namespace Grid.Selection
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class AreaMesh : MonoBehaviour
    {
        private Mesh _mesh;
        private MeshCollider _collider;
        private MeshFilter _meshFilter;
        private MeshRenderer _renderer;
        private List<Vector3> _vertices;
        private List<int> _triangles;

        private void Awake()
        {
            _vertices = new List<Vector3>();
            _triangles = new List<int>();

            _mesh = new Mesh();
            _mesh.name = "Selection Area Mesh";

            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = _mesh;

            _collider = GetComponent<MeshCollider>();
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Triangulate(HexMetrics metrics)
        {
            _vertices.Clear();
            _triangles.Clear();

            TriangulateCell(metrics);

            _mesh.vertices = _vertices.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.RecalculateNormals();

            _collider.sharedMesh = _mesh;

            _renderer.material.color = new Color(0, 255, 10);
        }

        private void TriangulateCell(HexMetrics metrics)
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

