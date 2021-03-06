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

        [SerializeField] private State.SelectionState _state;
        private Coordinates _coordinates;
        private CellMesh _mesh;
        private Metrics _metrics;
        private Pawn _pawn;

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

        public void LinkPawn(Pawn pawn)
        {
            _pawn = pawn;
        }
        
        public void UnlinkPawn()
        {
            _pawn = null;
        }

        public override string ToString()
        {
            return _coordinates.ToString();
        }
    }
}

