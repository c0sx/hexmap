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

        private void Awake()
        {
            _mesh = new Mesh();
            _mesh.name = "Selection Area Mesh";

            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = _mesh;

            _collider = GetComponent<MeshCollider>();
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Triangulate(List<HexCell> cells)
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            foreach (var cell in cells) {
                vertices.AddRange(cell.Mesh.vertices);
                triangles.AddRange(cell.Mesh.triangles);
            }

            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = triangles.ToArray();
            _mesh.RecalculateNormals();

            _collider.sharedMesh = _mesh;

            _renderer.material.color = new Color(0, 255, 10);
        }
    }
}

