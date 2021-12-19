using System.Collections.Generic;

using UnityEngine;

using Unit;

namespace Grid
{
    public class Turn : MonoBehaviour
    {
        [SerializeField] private List<Player> _players;
        [SerializeField] private string _currentTag;

        public string Tag => _currentTag;

        public void Next()
        {
            var first = _players[0];
            _players.Remove(first);
            _players.Add(first);

            _currentTag = first.tag;
        }

        public bool IsActivePlayer(Pawn pawn)
        {
            return _currentTag == pawn.tag;
        }
    }
}

