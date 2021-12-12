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
            var meshes = new CombineInstance[cells.Count];
            for (var i = 0; i < cells.Count; ++i) {
                var cell = cells[i];
                meshes[i].mesh = cell.MeshFilter.sharedMesh;
                meshes[i].transform = cell.transform.localToWorldMatrix;
            }

            _mesh.CombineMeshes(meshes, true, true);
            _mesh.RecalculateNormals();
            _mesh.Optimize();
            _collider.sharedMesh = _mesh;

            _renderer.material.color = new Color(0, 255, 5);
        }
    }
}

