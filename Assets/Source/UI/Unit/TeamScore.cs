using UnityEngine;
using UnityEngine.UI;

using Unit;

namespace UI.Unit
{
    public class TeamScore : MonoBehaviour
    {
        [SerializeField] private Team _team;
        [SerializeField] private Text _counter;
        [SerializeField] private Image _color;

        private void Start()
        {
            _team.PawnAdded += ChangeTeamCounter;
            ChangeTeamColor(_team.Primary);
        }

        private void Destroy()
        {
            _team.PawnAdded -= ChangeTeamCounter;
        }

        public void ChangeTeamCounter()
        {
            var value = _team.Count;
            _counter.text = value.ToString();
        }

        public void ChangeTeamColor(Color color)
        {
            _color.color = color;
        }
    }
}

