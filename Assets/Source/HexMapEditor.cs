using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    [SerializeField] private HexGrid _grid;
    [SerializeField] private List<Color> _colors;
    private Color _active;

    private void Awake()
    {
        SelectColor(0);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
            HandleInput();
        }
    }

    public void SelectColor(int index) 
    {
        _active = _colors[index];
    }

    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit)) {
			_grid.ColorCell(hit.point, _active);
		}
    }
}
