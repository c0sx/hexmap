using UnityEngine;

using Hexmap.Map.Grid;
using Hexmap.Rules;
using Hexmap.Unit;

[RequireComponent(typeof(Turns))]
public class Game : MonoBehaviour
{
    [SerializeField] private HexGrid _grid;

    private Turns _turns;

    private void Awake()
    {
        _turns = GetComponent<Turns>();
    }

    private void Start()
    {
        SubscribeGrid();
    }

    private void Destroy()
    {
        UnsubscribeGrid();
    }

    private void SubscribeGrid()
    {
        _grid.Started += OnGridStarted;
        _grid.PawnMoveEnds += OnPawnMoved;
    }

    private void UnsubscribeGrid()
    {
        _grid.Started -= OnGridStarted;
        _grid.PawnMoveEnds -= OnPawnMoved;
    }

    private void OnPawnMoved(Pawn pawn)
    {
        var turn = _turns.Next();
        _grid.SetTurn(turn);
    }

    private void OnGridStarted()
    {
        var turn = _turns.First();
        _grid.SetTurn(turn);
    }
}