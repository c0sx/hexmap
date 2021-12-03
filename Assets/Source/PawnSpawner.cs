using UnityEngine;

public class PawnSpawner : MonoBehaviour
{
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Color _color;

    public Pawn Spawn()
    {
        var pawn = Instantiate<Pawn>(_pawnPrefab);
        
        pawn.Init(_color);

        return pawn;
    }
}
