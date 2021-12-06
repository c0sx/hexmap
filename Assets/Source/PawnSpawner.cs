using UnityEngine;

public class PawnSpawner : MonoBehaviour
{
    [SerializeField] private int _size = 2;
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Color _current;
    [SerializeField] private Color _alternative;

    public int Size => _size;

    public Pawn Spawn()
    {
        var pawn = Instantiate<Pawn>(_pawnPrefab);
        
        pawn.Init(_current, _alternative);

        return pawn;
    }
}
