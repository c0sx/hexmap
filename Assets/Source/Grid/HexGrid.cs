using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Grid.Selection;
using Grid.Cell;

namespace Grid 
{
    [RequireComponent(typeof(HexCellSelector))]
    public class HexGrid: MonoBehaviour
    {
        [SerializeField] private float _outerRadius = 10f;
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;
        [SerializeField] private float _border = 0.5f;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private HexCell _cellPrefab;
        [SerializeField] private Text _labelPrefab;
        [SerializeField] private PawnSpawner _top;
        [SerializeField] private PawnSpawner _bottom;
        [SerializeField] private Area _area;

        private HexCellSelector _selector;
        private HexMetrics _metrics;
        private List<HexCell> _cells;
        private List<Pawn> _pawns;

        public List<HexCell> Cells => _cells;
        public int Width => _width;
        public int Height => _height;


        private void Awake()
        {
            _selector = GetComponent<HexCellSelector>();
            _metrics = new HexMetrics(_outerRadius, _border);
            _cells = new List<Cell.HexCell>(_height * _width);
            _pawns = new List<Pawn>();

            for (int z = 0, i = 0; z < _height; z++, i = 0) {
                for (int x = 0; x < _width; x++) {
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

        private void SpawnPawn(HexCell cell, PawnSpawner spawner)
        {
            var pawn = spawner.Spawn();
            _pawns.Add(pawn);

            cell.PlacePawn(pawn);
        }

        private HexCell CreateCell(int i, int x, int z)
        {
            var position = _metrics.GetPositionFor(i, x, z);

            var cell = Instantiate<HexCell>(_cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;

            var coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
            cell.Init(coordinates, _metrics);

            var label = Instantiate<Text>(_labelPrefab);
            label.rectTransform.SetParent(_canvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);        
            label.text = cell.ToString();

            return cell;
        }

        private void Subscribe()
        {
            foreach (var cell in _cells) {
                cell.Clicked += SelectCell;
            }

            foreach (var pawn in _pawns) {
                pawn.Clicked += SelectPawn;
            }
        }

        private void Unsubscribe()
        {
            foreach (var pawn in _pawns) {
                pawn.Clicked -= SelectPawn;
            }

            foreach (var cell in _cells) {
                cell.Clicked -= SelectCell;
            }
        }

        private void SelectPawn(Pawn pawn)
        {
            var cell = _cells.Find(one => one.HasPawn(pawn));
            if (cell != null) {
                SelectCell(cell);
            }
        }

        private void SelectCell(HexCell cell) {
            var selected = _selector.Select(this, cell);
            _area.Select(selected);
        }
    }
}


