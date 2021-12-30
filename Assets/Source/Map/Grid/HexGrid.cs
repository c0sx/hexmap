using System;
using System.Collections.Generic;

using UnityEngine;

using Map.Selection;
using Map.Cell;
using Map.Selector;

using Unit;

namespace Map.Grid 
{
    [RequireComponent(typeof(GridCellSelector), typeof(Turn), typeof(Options))]
    [RequireComponent(typeof(Cells), typeof(Spawners))]
    public class HexGrid: MonoBehaviour
    {
        public Action<Pawn> PawnMoved;
        public Action<Pawn> PawnEaten;

        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;
        [SerializeField] private Area _area;

        private Options _options;
        private Cells _cells;
        private Spawners _spawners;
        private Turn _turn;
        private GridCellSelector _selector;
        private List<Pawn> _pawns;

        public int Width => _options.Width;
        public int Height => _options.Height;


        private void Awake()
        {
            _options = GetComponent<Options>();
            _cells = GetComponent<Cells>();
            _spawners = GetComponent<Spawners>();

            _turn = GetComponent<Turn>();
            _selector = GetComponent<GridCellSelector>();

            _pawns = new List<Pawn>();
        }

        private void Start()
        {
            var metrics = new Metrics(_options);
            _cells.Create(metrics);

            _spawners.Create(this);
            _spawners.Spawn(this);

            Subscribe();
        }

        private void Destroy()
        {
            Unsubscribe();
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

        private void Subscribe()
        {
            _spawners.Subscribe();
            _spawners.Clicked += SelectPawn;

            // _area.PawnMoved += _turn.Next;
        }

        private void Unsubscribe()
        {
            _spawners.Unsubscribe();
            _spawners.Clicked -= SelectPawn;

            // _area.PawnMoved -= _turn.Next;
        }

        private void SelectPawn(Pawn pawn)
        {
            if (_turn.IsActivePlayer(pawn)) {
                var cell = _cells.FindWithPawn(pawn);
                if (cell != null) {
                    SelectCell(cell);
                }
            }
        }

        private void SelectCell(GridCell cell) 
        {
            var selected = _selector.Select(_turn, this, cell);
            _area.Select(selected);
        }
    }
}
