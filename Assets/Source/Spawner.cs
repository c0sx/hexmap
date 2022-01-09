using System.Collections.Generic;

using UnityEngine;

using Map.Cell;
using Unit;

public class Spawner : MonoBehaviour
{
    public int Size => _size;
    public List<Pawn> Pawns => _player.Pawns;
    public Player Player => _player;
    
    [SerializeField] private int _size = 2;
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private Pawn _queenPrefab;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Init(this);
    }

    public bool Contains(Pawn pawn)
    {
        return _player.Pawns.Contains(pawn);
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
        var queen = Instantiate(_queenPrefab);
        queen.tag = pawn.tag;
        
        _player.ReplaceToQueen(pawn, queen);
        return queen;
    }
}