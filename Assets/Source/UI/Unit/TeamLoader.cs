using System.Collections.Generic;

using UnityEngine;

using Unit;

namespace UI.Unit
{
    public class TeamLoader : MonoBehaviour
    {
        [SerializeField] private List<Team> _teams;
        [SerializeField] private TeamScore _prefab;

        private void Start()
        {
            foreach (var team in _teams) {
                var score = Instantiate(_prefab);
                score.transform.SetParent(transform);
                score.Init(team);
            }
        }
    }
}

