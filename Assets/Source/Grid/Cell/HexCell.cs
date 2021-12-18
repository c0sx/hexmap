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
        [SerializeField] private SelectionState _state;
        private Color _current;
        private HexCellMesh _mesh;
        private HexMetrics _metrics;
        private Pawn _pawn;

        public Mesh Mesh => _mesh.Mesh;
        public MeshFilter MeshFilter => _mesh.MeshFilter;
        public MeshRenderer MeshRenderer => _mesh.MeshRenderer;
        public bool Occupied => _pawn != null;
        
        private void OnMouseDown()
        {
            if (_state.IsClickable(this)) {
                Clicked?.Invoke(this);
            }
        }

        public void Init(HexCoordinates coordinates, HexMetrics metrics)
        {
            _coordinates = coordinates;
            _metrics = metrics;

            _mesh = GetComponent<HexCellMesh>();
            _state = GetComponent<SelectionState>();

            _state.Init(this);
        }

        public void Triangulate()
        {
            _mesh.Triangulate(_metrics, this);
        }

        public void Select()
        {
            _state.Select();
        }

        public void SelectPawn()
        {
            _pawn?.Select();
        }

        public void Deselect()
        {
            _state.Deselect();
            _pawn?.Deselect();
        }

        public void PlacePawn(Pawn pawn)
        {
            _pawn = pawn;

            _pawn.transform.SetParent(transform);
            _pawn.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        
        public void MovePawn(HexCell to)
        {
            to.PlacePawn(_pawn);
            _pawn = null;
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

