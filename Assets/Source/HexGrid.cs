using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HexGrid: MonoBehaviour
{
    [SerializeField] private float _outerRadius = 10f;
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 6;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private HexMesh _hexMesh;
    [SerializeField] private HexCell _cellPrefab;
    [SerializeField] private Text _labelPrefab;

    private HexMetrics _metrics;

    private List<HexCell> _cells;

    private void Awake()
    {
        _metrics = new HexMetrics(_outerRadius);

        _cells = new List<HexCell>(_height * _width);

        for (int z = 0; z < _height; z++) {
            for (int x = 0; x < _width; x++) {
                var cell = createCell(x, z);
                _cells.Add(cell);
            }
        }

    }

    private void Start()
    {
        _hexMesh.Triangulate(_metrics, _cells);
    }

    private HexCell createCell(int x, int z)
    {
        var position = _metrics.GetPositionFor(x, z);

        var cell = Instantiate<HexCell>(_cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        var label = Instantiate<Text>(_labelPrefab);
        label.rectTransform.SetParent(_canvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x.ToString() + ':' + z.ToString();

        return cell;
    }
}
