using UnityEngine;

using Hexmap.Unit;

namespace Hexmap.Map
{
    public class Turn : MonoBehaviour
    {
        private string _currentTag;
        private Player _currentPlayer;

        public void Change(TurnOptions options)
        {
            _currentTag = options.PlayerTag;
            _currentPlayer = options.Player;
        }

        public bool IsActivePlayer(Pawn pawn)
        {
            return _currentTag == pawn.tag;
        }
    }
}
