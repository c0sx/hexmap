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
        _cells.ForEach(cell => {
            cell.Triangulate();
        });
    }

    private HexCell createCell(int i, int x, int z)
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
