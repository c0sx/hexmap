using System.Collections.Generic;

using UnityEngine;

using Map;

namespace Rules
{
    public class Turns : MonoBehaviour
    {
        [SerializeField] private Player _top;
        [SerializeField] private Player _bottom;
        private List<Player> _players;

        private int _turn;

        private void Awake()
        {
            _players = new List<Player>() { _bottom, _top };
        }

        private void Start()
        {
            if (_players.Count != 2) {
                throw new System.Exception("You should add 2 players");
            }
        }

        public TurnOptions First()
        {
            _turn = 0;
            return Create();
        }

        public TurnOptions Next()
        {
            _turn += 1;
            return Create();
        }

        private TurnOptions Create()
        {
            var player = GetPlayerForTurn(_turn);
            return new TurnOptions(_turn, player);
        }

        private Player GetPlayerForTurn(int turn)
        {
            var index = turn % 2 == 0 ? 0 : 1;
            return _players[index];
        }
    }
}

