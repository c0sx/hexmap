using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Map.Grid;
using Map.Movement.Selector;
using Unit;

namespace Map.Movement
{
    [RequireComponent(typeof(GridCellSelector))]
    public class Area : MonoBehaviour
    {
        public event Action<GridCellSelection> MovingTargetSelected;

        [SerializeField] private HexGrid _grid;
        
        private List<GridCellSelection> _vectors;
        private GridCellSelector _selector;

        private void Awake()
        {
            _selector = GetComponent<GridCellSelector>();
            _vectors = new List<GridCellSelection>();
        }

        public void GenerateSelection(Pawn pawn)
        {
            ClearSelection();

            _vectors = _selector.SelectAvailableVectors(_grid, pawn);
            foreach (var vector in _vectors) {
                foreach (var node in vector.Vector) {
                    if (node.Occupied) {
                        continue;
                    }

                    node.Select();
                }

                Subscribe(vector);
            }
        }

        public bool HasEatingVectors(HexGrid grid, Pawn pawn)
        {
            var vectors = _selector.GetEatingVectors(grid, pawn);
            return vectors.Count > 0;
        }

        public void ClearSelection()
        {
            foreach (var vector in _vectors) {
                Unsubscribe(vector);

                foreach (var cell in vector.Vector) {
                    cell.Deselect();
                }
            }
        }

        private void Subscribe(GridCellSelection selection)
        {
            foreach (var cell in selection.Vector) {
                cell.Clicked += OnTargetCellClicked;
            }
        }

        private void Unsubscribe(GridCellSelection selection) 
        {
            foreach (var cell in selection.Vector) {
                cell.Clicked -= OnTargetCellClicked;
            }
        }

        private void OnTargetCellClicked(GridCell target)
        {
            var vector = _vectors.Find(vector => vector.Contains(target));
            if (vector == null) {
                throw new Exception("Target cell for movement not found");
            }

            vector.Select(target);
            MovingTargetSelected?.Invoke(vector);
        }
    }
}
