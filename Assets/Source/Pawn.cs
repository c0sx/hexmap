using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Pawn : MonoBehaviour
{
    private MeshRenderer _mesh;
    private Color _current;
    private Color _previous;

    private void OnMouseDown()
    {
        ToggleSelection();
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
        (_previous, _current) = (_current, _previous);
        _mesh.material.color = _current;
    }
}
