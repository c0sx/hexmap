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

    private HexCell[] _cells;

    private void Awake()
    {
        _cells = new HexCell[_height * _width];

        for (int z = 0, i = 0; z < _height; z++) {
            for (int x = 0; x < _width; x++) {
                var cell = createCell(x, z);
                _cells[i] = cell;
                i++;
            }
        }

    }

    private HexCell createCell(int x, int z)
    {
        var position = new Vector3(x * _outerRadius, 0f, z * _outerRadius);

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
