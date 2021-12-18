using System;

using UnityEngine;

namespace Grid.Cell
{
    [RequireComponent(typeof(HexCellMesh), typeof(SelectionState))]
    public class HexCell : MonoBehaviour
    {
        public Action<HexCell> Clicked;

        [SerializeField] private HexCoordinates _coordinates;
        [SerializeField] private Pawn _pawnPrefab;
        private SelectionState _state;
        private Color _current;
        private HexCellMesh _mesh;
        private HexMetrics _metrics;
        private Pawn _pawn;

        public Mesh Mesh => _mesh.Mesh;
        public MeshFilter MeshFilter => _mesh.MeshFilter;
        
        private void OnMouseDown()
        {
            if (_pawn != null) {
                Clicked?.Invoke(this);
            }
        }

        public void Init(HexCoordinates coordinates, HexMetrics metrics)
        {
            _coordinates = coordinates;
            _metrics = metrics;

            _mesh = GetComponent<HexCellMesh>();
            _state = GetComponent<SelectionState>();
            _state.Init(_mesh.MeshRenderer);
        }

        public void Triangulate()
        {
            _mesh.Triangulate(_metrics, this);
        }

        public void Select()
        {
            _state.Select(_mesh.MeshRenderer);
        }

        public void SelectPawn()
        {
            _pawn?.Select();
        }

        public void Deselect()
        {
            _state.Deselect(_mesh.MeshRenderer);
            _pawn?.Deselect();
        }


        public void PlacePawn(Pawn pawn)
        {
            _pawn = pawn;

            _pawn.transform.SetParent(transform);
            _pawn.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        public bool HasPawn(Pawn pawn)
        {
            if (!_pawn) {
                return false;
            }

            return _pawn == pawn;
        }

        public override string ToString()
        {
            return _coordinates.ToString();
        }
    }

}

