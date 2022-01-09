using UnityEngine;
using UnityEngine.UI;

namespace Hexmap.UI.Unit
{
    public class PawnsCounter : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Text _counter;
        [SerializeField] private Image _color;

        private void Start()
        {
            _player.PawnAdded += ChangeTeamCounter;
            _player.PawnRemoved += ChangeTeamCounter;

            ChangeTeamColor(_player.Primary);
        }

        private void Destroy()
        {
            _player.PawnAdded -= ChangeTeamCounter;
            _player.PawnRemoved -= ChangeTeamCounter;
        }

        public void ChangeTeamCounter()
        {
            var value = _player.Count;
            _counter.text = value.ToString();
        }

        public void ChangeTeamColor(Color color)
        {
            _color.color = color;
        }
    }
}

