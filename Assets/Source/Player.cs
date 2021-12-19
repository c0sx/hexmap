using UnityEngine;

using Unit;

[RequireComponent(typeof(Team))]
public class Player : MonoBehaviour
{
    private Team _team;

    private void Start()
    {
        _team = GetComponent<Team>();
    }

    public void Add(Pawn pawn)
    {
        _team.Add(pawn);
    }
}
