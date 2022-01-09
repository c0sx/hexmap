using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Unit;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _size = 2;
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Player _player;

    public int Size => _size;
    public List<Pawn> Pawns => _player.Pawns;

    private void Start()
    {
        _player.Init(this);
    }

    public void Place(GridCell cell)
    {
        transform.position = cell.transform.position;
    }

    public Pawn SpawnPawn()
    {
        var pawn = Instantiate(_pawnPrefab);
        _player.Add(pawn);

        return pawn;
    }

    public Pawn SpawnQueen(Pawn pawn)
    {
        var queen = Instantiate(_pawnPrefab);
        queen.tag = pawn.tag;
        _player.ReplaceToQueen(pawn, queen);

        return queen;
    }
}