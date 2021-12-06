using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Pawn : MonoBehaviour
{
    private MeshRenderer _mesh;
    [SerializeField] private Color _current;
    [SerializeField] private Color _previous;

    private void OnMouseDown()
    {
        var inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit)) {
            ToggleSelection();
        }
    }

    public void Init(Color color, Color previous)
    {
        _current = color;
        _previous = previous;

        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = color;
    }

    private void ToggleSelection()
    {
        var temp = _previous;
        _previous = _current;
        _current = temp;

        _mesh.material.color = _current;
    }
}
