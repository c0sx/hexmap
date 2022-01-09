using UnityEngine;

using Unit;

namespace Map
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
            return pawn.CompareTag(_currentTag);
        }
    }
}
