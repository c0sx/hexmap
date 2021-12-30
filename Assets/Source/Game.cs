using UnityEngine;

using Map.Grid;
using Rules;
using Unit;

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
        Debug.Log("Game is started");

        Subscribe();

        var turn = _turns.First();
        _grid.SetTurn(turn);
    }

    private void Destroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _grid.PawnMoved += OnPawnMoved;
    }

    private void Unsubscribe()
    {
        _grid.PawnMoved -= OnPawnMoved;
    }

    private void OnPawnMoved(Pawn pawn)
    {
        Debug.Log("OnPawnMoved");
    }
}
