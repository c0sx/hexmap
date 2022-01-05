using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Movement;
using Map.Cell;

using Unit;

namespace Map.Grid 
{
    [RequireComponent(typeof(Turn), typeof(Options))]
    [RequireComponent(typeof(Cells), typeof(Spawners))]
    [RequireComponent(typeof(Area))]
    public class HexGrid: MonoBehaviour
    {
        public Action Started;
        public Action<Pawn> PawnMoved;
        public Action<Pawn> PawnEaten;

        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;

        private Options _options;
        private Cells _cells;
        private Spawners _spawners;
        private Turn _turn;
        private Area _area;
        private List<Pawn> _pawns;

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

        private void SubscribePawns()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked += OnSelectPawn;
                pawn.Died += OnPawnDied;
            }
        }

        private void UnsubscribePawns()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked -= OnSelectPawn;
                pawn.Died -= OnPawnDied;
            }
        }

        private void SubscribeArea()
        {
            _area.PawnMoved += OnPawnMoved;
            _area.PawnEaten += OnPawnEaten;
        }

        private void UnsubscribeArea()
        {
            _area.PawnMoved -= OnPawnMoved;
            _area.PawnEaten -= OnPawnEaten;
        }

        private void OnSelectPawn(Pawn pawn)
        {
            if (!_turn.IsActivePlayer(pawn)) {
                return;
            }


            // deselect other pawns
            foreach (var other in _pawns) {
                other.Deselect();
            }

            // select this pawn
            pawn.Select();

            // create moving area
            _area.Select(pawn);
        }

        private void OnPawnDied(Pawn pawn)
        {
            _pawns.Remove(pawn);
        }

        private void OnPawnMoved(Pawn pawn)
        {
            PawnMoved?.Invoke(pawn);
        }

        private void OnPawnEaten(Pawn pawn)
        {
            PawnEaten?.Invoke(pawn);
        }
    }
}
