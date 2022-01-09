using System;
using System.Collections.Generic;

using UnityEngine;

using Hexmap.Unit;

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

        pawn.Died += OnPawnDied;

        PawnAdded?.Invoke();
    }

    private void OnPawnDied(Pawn pawn)
    {
        pawn.Died -= OnPawnDied;

        _pawns.Remove(pawn);
        PawnRemoved?.Invoke();
    }
}