using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private HexCoordinates _coordinates;

    public void Init(HexCoordinates coordinates)
    {
        _coordinates = coordinates;
    }

    public override string ToString()
    {
        return _coordinates.ToString();
    }
}
