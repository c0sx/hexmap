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
            // remove input listener from previous player
            BeforeChanged(_currentPlayer);

            // change player
            _currentTag = options.PlayerTag;
            _currentPlayer = options.Player;

            // subscribe current player to input listener
            AfterChanged(_currentPlayer);
        }

        public bool IsActivePlayer(Pawn pawn)
        {
            return _currentTag == pawn.tag;
        }

        private void BeforeChanged(Player previous)
        {
            previous?.Unsubscribe();
        }

        private void AfterChanged(Player current)
        {
            current.Subscribe();
        }
    }
}
