using System.Collections.Generic;

using UnityEngine;

using Map.Selection;
using Map.Cell;
using Map.Selector;
using Unit;

namespace Map.Grid 
{
    [RequireComponent(typeof(HexCellSelector), typeof(Turn), typeof(Options))]
    public class HexGrid: MonoBehaviour
    {
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private Spawner _top;
        [SerializeField] private Spawner _bottom;
        [SerializeField] private Area _area;

        private Options _options;
        private Metrics _metrics;

        private Turn _turn;
        private HexCellSelector _selector;
        private List<GridCell> _cells;
        private List<Pawn> _pawns;

        public List<GridCell> Cells => _cells;

        public int Width => _options.Width;
        public int Height => _options.Height;


        private void Awake()
        {
            _options = GetComponent<Options>();
            _metrics = new Metrics(_options);


            _turn = GetComponent<Turn>();
            _selector = GetComponent<HexCellSelector>();

            _cells = new List<Cell.GridCell>(_options.GridSize);
            _pawns = new List<Pawn>();

            for (int z = 0, i = 0; z < _options.Height; z++, i = 0) {
                for (int x = 0; x < _options.Width; x++) {
                    var cell = CreateCell(i, x, z);
                    _cells.Add(cell);
                    i++;
                }
            }
        }

        private void Start()
        {
            foreach (var cell in _cells) {
                cell.Triangulate();
            }

            PlaceSpawners();
            SpawnPawns();
            Subscribe();
            SetTurn();
        }

        private void Destroy()
        {
            Unsubscribe();
        }

        private void PlaceSpawners()
        {
            var first = _cells[0];
            _bottom.transform.position = first.transform.position;

            var last = _cells[_cells.Count - 1];
            _top.transform.position = last.transform.position;
        }

        private void SpawnPawns()
        {
            var bottomSlice = _cells.GetRange(0, _bottom.Size * Width);
            foreach (var cell in bottomSlice) {
                SpawnPawn(cell, _bottom);
            }

            var length = _top.Size * Width;
            var offset = _cells.Count - length;
            var topSlice = _cells.GetRange(offset, length);
            foreach (var cell in topSlice) {
                SpawnPawn(cell, _top);
            }
        }

        private void SpawnPawn(GridCell cell, Spawner spawner)
        {
            var pawn = spawner.Spawn();
            _pawns.Add(pawn);

            cell.PlacePawn(pawn);
        }

        private GridCell CreateCell(int i, int x, int z)
        {
            var position = _metrics.GetPositionFor(i, x, z);

            var cell = Instantiate<GridCell>(_cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;

            var coordinates = Coordinates.FromOffsetCoordinates(x, z);
            cell.Init(coordinates, _metrics);

            return cell;
        }

        private void Subscribe()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked += SelectPawn;
            }

            _area.PawnMoved += _turn.Next;
        }

        private void Unsubscribe()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked -= SelectPawn;
            }

            _area.PawnMoved -= _turn.Next;
        }

        private void SelectPawn(Pawn pawn)
        {
            if (_turn.IsActivePlayer(pawn)) {
                var cell = _cells.Find(one => one.HasPawn(pawn));
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

        private void SetTurn()
        {
            _turn.Next();
        }
    }
}

