using UnityEngine;

using Unit;

namespace Map
{
    public class Turn : MonoBehaviour
    {
        private string _currentTag;

        public void Change(TurnOptions options)
        {
            _currentTag = options.PlayerTag;
        }

        public bool IsActivePlayer(Pawn pawn)
        {
            return _currentTag == pawn.tag;
        }
    }
}
