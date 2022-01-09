using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Map.Movement;
using Map.Movement.Selector;
using Unit;

namespace Map.Grid 
{
    [RequireComponent(typeof(Turn), typeof(Options))]
    [RequireComponent(typeof(Spawners))]
    [RequireComponent(typeof(Area), typeof(Rows))]
    public class HexGrid: MonoBehaviour
    {
        public event Action Started;
        public event Action<Pawn> PawnMoveEnds;
        
        public int Width => _options.Width;

        [SerializeField] private Pawn _selectedPawn;

        private Options _options;
        private Rows _rows;
        private Spawners _spawners;
        private Turn _turn;
        private Area _area;
        private Cells _cells;

        private void Awake()
        {
            _options = GetComponent<Options>();
            _rows = GetComponent<Rows>();
            _spawners = GetComponent<Spawners>();
            _area = GetComponent<Area>();
            _turn = GetComponent<Turn>();
        }

        private void Start()
        {
            var cells = _rows.CreateCell(_options);
            _cells = new Cells(cells);
            
            _spawners.Create(_cells);
            _spawners.Spawn(this);

            SubscribePawns();
            SubscribeArea();

            Started?.Invoke();
        }

        private void OnDestroy()
        {
            UnsubscribePawns();
            UnsubscribeArea();
        }

        public void SetTurn(TurnOptions turn)
        {
            _turn.Change(turn);
        }

        public List<GridCell> GetNFirst(int size) => _cells.GetNFirst(size);
        
        public List<GridCell> GetNLast(int size) =>  _cells.GetNLast(size);
        
        public GridCell FindByVector2(Vector2Int vector) {
            var coordinates = Coordinates.FromVector2(vector);
            return _cells.FindByCoordinates(coordinates);
        }

        private void SubscribePawns()
        {
            var pawns = _spawners.GetPawns();
            foreach (var pawn in pawns) {
                SubscribePawn(pawn);
            }
        }

        private void SubscribePawn(Pawn pawn)
        {
            pawn.Selected += OnPawnSelected;
            pawn.Moved += OnPawnMoved;
            pawn.Eats += OnPawnEats;
        }

        private void UnsubscribePawns()
        {
            var pawns = _spawners.GetPawns();
            foreach (var pawn in pawns)
            {
                UnsubscribePawn(pawn);
            }
        }

        private void UnsubscribePawn(Pawn pawn)
        {
            pawn.Selected -= OnPawnSelected;
            pawn.Moved -= OnPawnMoved;
            pawn.Eats -= OnPawnEats;
        }

        private void SubscribeArea()
        {
            _area.MovingTargetSelected += OnMovingTargetSelected;
            _area.QueenReached += OnQueenReached;
        }

        private void UnsubscribeArea()
        {
            _area.MovingTargetSelected -= OnMovingTargetSelected;
            _area.QueenReached += OnQueenReached;
        }

        private void OnPawnSelected(Pawn pawn)
        {
            if (!_turn.IsActivePlayer(pawn)) {
                return;
            }

            var pawns = _spawners.GetPawns();
            Debug.Log("1");
            foreach (var other in pawns) {
                other.Deselect();
            }

            _selectedPawn = pawn;
            pawn.Select();

            _area.GenerateSelection(pawn);
        }

        private void OnPawnEats(Pawn pawn)
        {
            var hasEatingVectors = _area.HasEatingVectors(this, pawn);
            if (!hasEatingVectors) {
                pawn.Deselect();
                _area.ClearSelection();
                PawnMoveEnds?.Invoke(pawn);
                return;
            }

            _area.GenerateSelection(pawn);
        }

        private void OnMovingTargetSelected(GridCellSelection target)
        {
            if (!_selectedPawn) {
                throw new Exception("Pawn for moving not selected");
            }

            _selectedPawn.Move(target);
        }

        private void OnPawnMoved(Pawn pawn, GridCell oldPosition, GridCellSelection newPosition)
        {
            var enemy = newPosition.Vector.Find(one => one.Occupied && pawn.IsEnemy(one.Pawn));
            if (enemy != null) {
                pawn.Eat(enemy.Pawn);
                return;
            }

            pawn.Deselect();
            _area.ClearSelection();
            PawnMoveEnds?.Invoke(pawn);
        }

        private void OnQueenReached(GridCell cell)
        {
            var queen = _spawners.SpawnQueen(cell.Pawn);
            SubscribePawn(queen);
            
            Debug.Log("Queen Reached");
        }
    }
}