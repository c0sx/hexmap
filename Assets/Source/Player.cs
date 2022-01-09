using System;
using System.Collections.Generic;

using UnityEngine;

using Unit;

public class Player : MonoBehaviour
{
    public event Action PawnAdded;
    public event Action PawnRemoved;

    [SerializeField] private List<Pawn> _pawns;
    [SerializeField] private Color _primary;
    [SerializeField] private Color _selected;
    [SerializeField] private int _direction;

    public Color Primary => _primary;
    public Color Selected => _selected;
    public int Count => _pawns.Count;
    public int Direction => _direction;
    public List<Pawn> Pawns => _pawns;
    
    private void Awake()
    {
        _pawns = new List<Pawn>();
    }

    public void Init(Spawner spawner)
    {
        gameObject.tag = spawner.tag;
    }

    public void Add(Pawn pawn)
    {
        pawn.AssignPlayer(this);
        _pawns.Add(pawn);

        pawn.Died += OnPawnDied;

        PawnAdded?.Invoke();
    }

    public void ReplaceToQueen(Pawn pawn, Pawn queen)
    {
        pawn.Died -= OnPawnDied;
        _pawns.Remove(pawn);
        _pawns.Add(queen);

        queen.Died += OnPawnDied;
    }

    private void OnPawnDied(Pawn pawn)
    {
        pawn.Died -= OnPawnDied;

        _pawns.Remove(pawn);
        PawnRemoved?.Invoke();
    }
}