using UnityEngine;

using Unit;

namespace UI.Unit
{

    public class TeamScore : MonoBehaviour
    {
        [SerializeField] private Team _team;

        public void Init(Team team)
        {
            _team = team;
        }
    }
}

