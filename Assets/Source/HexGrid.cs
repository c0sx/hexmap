using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HexGrid: MonoBehaviour
{
    [SerializeField] private float _outerRadius = 10f;
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 6;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private HexCell _cellPrefab;
    [SerializeField] private Text _labelPrefab;
    [SerializeField] private Color _default;
    [SerializeField] private Color _touched;

    private HexMetrics _metrics;

    private List<HexCell> _cells;

    private void Awake()
    {
        _metrics = new HexMetrics(_outerRadius);

        _cells = new List<HexCell>(_height * _width);

        for (int z = 0, i = 0; z < _height; z++, i = 0) {
            for (int x = 0; x < _width; x++) {
                var cell = createCell(i, x, z);
                _cells.Add(cell);
                i++;
            }
        }
    }

    private void Start()
    {
        foreach (var cell in _cells) {
            cell.Triangulate(_metrics);
        }
    }

    private HexCell createCell(int i, int x, int z)
    {
        var position = _metrics.GetPositionFor(i, x, z);

        var cell = Instantiate<HexCell>(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.position = position;
        cell.Init(HexCoordinates.FromOffsetCoordinates(x, z), _default);

        // var label = Instantiate<Text>(_labelPrefab);
        // label.rectTransform.SetParent(_canvas.transform, false);
        // label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        // label.text = cell.ToString();

        return cell;
    }

    private void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);

        var coordinates = HexCoordinates.FromPosition(position, _metrics);
        int index = coordinates.X + coordinates.Z * _width + coordinates.Z / 2;
		HexCell cell = _cells[index];
        cell.Touch(_touched);
    }
}
