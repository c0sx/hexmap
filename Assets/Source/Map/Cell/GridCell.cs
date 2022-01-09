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
        private Pawn _pawn;

        public Coordinates Coordinates => _coordinates;
        public Pawn Pawn => _pawn;
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
            _isQueen = z == 0 || z == metrics.Height - 1;
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

