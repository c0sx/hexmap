using UnityEngine;

public class PawnSpawner : MonoBehaviour
{
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Color _current;
    [SerializeField] private Color _alternative;

    public Pawn Spawn()
    {
        var pawn = Instantiate<Pawn>(_pawnPrefab);
        
        pawn.Init(_current, _alternative);

        return pawn;
    }
}
