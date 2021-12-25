using UnityEngine;

using Unit;

[RequireComponent(typeof(Player))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private int _size = 2;
    [SerializeField] private Pawn _pawnPrefab;

    private Player _player;

    public int Size => _size;

    private void Start()
    {
        _player = GetComponent<Player>();
        _player.tag = gameObject.tag;
    }

    public Pawn Spawn()
    {
        var pawn = Instantiate<Pawn>(_pawnPrefab);
        pawn.tag = gameObject.tag;
        _player.Add(pawn);

        return pawn;
    }
}


