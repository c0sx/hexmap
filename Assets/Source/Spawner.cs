using UnityEngine;

using Map.Cell;
using Unit;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _size = 2;
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Player _player;

    public int Size => _size;

    private void Start()
    {
        _player.tag = gameObject.tag;
    }

    public void Place(GridCell cell)
    {
        transform.position = cell.transform.position;
    }

    public Pawn Spawn()
    {
        var pawn = Instantiate<Pawn>(_pawnPrefab);
        _player.Add(pawn);

        return pawn;
    }
}


