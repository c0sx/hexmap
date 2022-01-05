using System;

using UnityEngine;

using Map.Grid;
using Unit;

namespace Map.Cell
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
        public Coordinates Coordinates => _coordinates;
        public Pawn Pawn => _pawn;
        public bool Occupied => _pawn != null;

        private void Awake()
        {
            _mesh = GetComponent<CellMesh>();
            _state = GetComponent<State.SelectionState>();
        }

        private void Start()
        {
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
            _coordinates = coordinates;
            _metrics = metrics;
            _state.Init(this);
        }

        public void Select()
        {
            _state.Select();
        }

        public void Deselect()
        {
            _state.Deselect();
        }

        public void PlacePawn(Pawn pawn)
        {
            _pawn = pawn;
            _pawn.Place(this);
        }
        
        public void RemovePawn()
        {
            _pawn = null;
        }

        public Vector2Int AxisWith(GridCell to)
        {
            return _coordinates.AxisWith(to.Coordinates);
        }

        public override string ToString()
        {
            return _coordinates.ToString();
        }
    }

}

