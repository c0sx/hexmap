using System;

using UnityEngine;

using Map.Grid;
using Unit;

namespace Map.Cell
{
    [RequireComponent(typeof(GridCellMesh), typeof(Selection))]
    public class GridCell : MonoBehaviour
    {
        public event Action<GridCell> Clicked;
        public event Action<GridCell> QueenReached;

        private Selection _selection;
        private Coordinates _coordinates;
        private GridCellMesh _mesh;
        private Metrics _metrics;
        private bool _isQueen;
        private int _direction;
        private Pawn _pawn;
        private PawnUnit _unit;

        public Coordinates Coordinates => _coordinates;
        public Pawn Pawn => _pawn;
        public int Direction => _direction;
        public bool Occupied => _pawn != null;

        private void Awake()
        {
            _mesh = GetComponent<GridCellMesh>();
            _selection = GetComponent<Selection>();
        }

        private void Start()
        {
            _selection.Deselect(_isQueen);
            _mesh.Triangulate(_metrics);
        }
        
        private void OnMouseDown()
        {
            if (IsClickable()) {
                Clicked?.Invoke(this);
            }
        }

        public void Init(Metrics metrics, int x, int z)
        {
            _metrics = metrics;
            
            transform.localPosition = metrics.GetPositionFor(x, z);

            _coordinates = Coordinates.FromOffsetCoordinates(x, z);
            _direction = z == 0 ? 1 : z == metrics.Height - 1 ? -1 : 0;
            _isQueen = _direction != 0;
        }

        public void Select()
        {
            _selection.Select();
        }

        public void Deselect()
        {
            _selection.Deselect(_isQueen);
        }

        public void LinkPawn(Pawn pawn)
        {
            _pawn = pawn;
        }

        public void UnlinkPawn()
        {
            _pawn = null;
        }

        public void PlacePawn(Pawn pawn)
        {
            LinkPawn(pawn);
            
            if (_isQueen)
            {
                QueenReached?.Invoke(this);
            }
        }

        public void PlaceUnit(PawnUnit unit)
        {
            _unit = unit;
            
            unit.PlaceTo(transform);
        }
        
        private bool IsClickable()
        {
            if (!_selection.IsSelected())
            {
                return !Occupied;
            }

            return true;
        }
    }
}

