using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Pawn : MonoBehaviour
{
    private MeshRenderer _mesh;

    public void Init(Color color)
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material.color = color;;
    }
}
