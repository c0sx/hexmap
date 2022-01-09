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
    [RequireComponent(typeof(Cells), typeof(Spawners))]
    [RequireComponent(typeof(Area))]
    public class HexGrid: MonoBehaviour
    {
        public Action Started;
        public Action<Pawn> PawnMoveEnds;

        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;

        private Options _options;
        private Cells _cells;
        private Spawners _spawners;
        private Turn _turn;
        private Area _area;
        private List<Pawn> _pawns;
        [SerializeField] private Pawn _selectedPawn;

        public int Width => _options.Width;
        public int Height => _options.Height;


        private void Awake()
        {
            _options = GetComponent<Options>();
            _cells = GetComponent<Cells>();
            _spawners = GetComponent<Spawners>();
            _area = GetComponent<Area>();

            _turn = GetComponent<Turn>();

            _pawns = new List<Pawn>();
        }

        private void Start()
        {
            var metrics = new Metrics(_options);
            _cells.Create(metrics);

            _spawners.Create(this);
            _pawns = _spawners.Spawn(this);

            SubscribePawns();
            SubscribeArea();

            Started?.Invoke();
        }

        private void Destroy()
        {
            UnsubscribePawns();
            UnsubscribeArea();
        }

        public void SetTurn(TurnOptions turn)
        {
            _turn.Change(turn);
        }

        public GridCell FirstCell() => _cells.First();

        public List<GridCell> GetNFirst(int size) => _cells.GetNFirst(size);
        
        public GridCell LastCell() => _cells.Last();
        
        public List<GridCell> GetNLast(int size) =>  _cells.GetNLast(size);
        
        public int GetMinX() => _cells.GetMinX();
        
        public int GetMaxX() => _cells.GetMaxX();
        
        public int GetMinZ() => _cells.GetMinZ();
        
        public int GetMaxZ() => _cells.GetMaxZ();

        public GridCell FindByCoordinates(Coordinates coordinate) => _cells.FindByCoordinates(coordinate);
        public GridCell FindByVector2(Vector2Int vector) {
            var coordinates = Coordinates.FromVector2(vector);
            return FindByCoordinates(coordinates);
        }

        private void SubscribePawns()
        {
            foreach (var pawn in _pawns) {
                pawn.Selected += OnPawnSelected;
                pawn.Moved += OnPawnMoved;
                pawn.Died += OnPawnDied;
                pawn.Eats += OnPawnEats;
            }
        }

        private void UnsubscribePawns()
        {
            foreach (var pawn in _pawns) {
                pawn.Selected -= OnPawnSelected;
                pawn.Moved -= OnPawnMoved;
                pawn.Died -= OnPawnDied;
                pawn.Eats -= OnPawnEats;
            }
        }

        private void SubscribeArea()
        {
            _area.MovingTargetSelected += OnMovingTargetSelected;
        }

        private void UnsubscribeArea()
        {
            _area.MovingTargetSelected -= OnMovingTargetSelected;
        }

        private void OnPawnSelected(Pawn pawn)
        {
            if (!_turn.IsActivePlayer(pawn)) {
                return;
            }

            foreach (var other in _pawns) {
                other.Deselect();
            }

            _selectedPawn = pawn;
            pawn.Select();

            _area.GenerateSelection(pawn);
        }

        private void OnPawnDied(Pawn pawn)
        {
            _pawns.Remove(pawn);
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
    }
}