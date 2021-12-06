using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

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

    private HexMetrics _metrics;
    private List<HexCell> _cells;

    private void Awake()
    {
        _metrics = new HexMetrics(_outerRadius, _border);

        _cells = new List<HexCell>(_height * _width);

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

        SpawnCells();
    }

    private void SpawnCells()
    {
        for (var i = 0; i < _width * _bottom.Size; ++i) {
            var cell = _cells[i];
            SpawnCell(cell, _bottom);
        }

        for (int i = _cells.Count - 1, counter = 0; counter < _width * _top.Size; --i, ++counter) {
            var cell = _cells[i];
            SpawnCell(cell, _top);
        }
    }

    private void SpawnCell(HexCell cell, PawnSpawner spawner)
    {
        var pawn = spawner.Spawn();
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
}
