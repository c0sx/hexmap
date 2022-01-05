using System;
using System.Collections.Generic;

using UnityEngine;

using Unit;

public class Player : MonoBehaviour
{
    public Action PawnAdded;
    public Action PawnRemoved;

    [SerializeField] private List<Pawn> _pawns;
    [SerializeField] private Color _primary;
    [SerializeField] private Color _selected;
    [SerializeField] private int _direction;

    public Color Primary => _primary;
    public Color Selected => _selected;
    public int Count => _pawns.Count;
    public int Direction => _direction;

    private void Awake()
    {
        _pawns = new List<Pawn>();
    }

    public void Add(Pawn pawn)
    {
        pawn.AssignPlayer(this);
        _pawns.Add(pawn);

        PawnAdded?.Invoke();
    }

    public void Subscribe()
    {
        foreach (var pawn in _pawns) {
            pawn.Died += OnPawnDied;
        }
    }

    public void Unsubscribe()
    {
        foreach (var pawn in _pawns) {
            pawn.Died -= OnPawnDied;
        }
    }

    private void OnPawnDied(Pawn pawn)
    {
        _pawns.Remove(pawn);
        PawnRemoved?.Invoke();
    }
}
