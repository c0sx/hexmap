using System;

using UnityEngine;

using Unit;

namespace Grid.Cell
{
    [RequireComponent(typeof(CellMesh), typeof(State.SelectionState))]
    public class GridCell : MonoBehaviour
    {
        public Action<GridCell> Clicked;

        [SerializeField] private Coordinates _coordinates;
        [SerializeField] private Pawn _pawnPrefab;
        [SerializeField] private State.SelectionState _state;
        private Color _current;
        private CellMesh _mesh;
        private Metrics _metrics;
        private Pawn _pawn;

        public Mesh Mesh => _mesh.Mesh;
        public MeshFilter MeshFilter => _mesh.MeshFilter;
        public MeshRenderer MeshRenderer => _mesh.MeshRenderer;
        public bool Occupied => _pawn != null;

        private void Awake()
        {
            _mesh = GetComponent<CellMesh>();
            _state = GetComponent<State.SelectionState>();
        }

        private void Start()
        {
            Debug.Log("Start");
            _mesh.Triangulate(_metrics);
        }
        
        private void OnMouseDown()
        {
            if (_state.IsClickable(this)) {
                Clicked?.Invoke(this);
            }
        }

        public void Init(Coordinates coordinates, Metrics metrics)
        {
            Debug.Log("Init");
            _coordinates = coordinates;
            _metrics = metrics;
            _state.Init(this);
        }

        public void Triangulate()
        {
            Debug.Log("Triangulate");
            _mesh.Triangulate(_metrics);
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
        
        public void MovePawn(GridCell to)
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

