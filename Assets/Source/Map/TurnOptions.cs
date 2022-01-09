namespace Map
{
    public class TurnOptions
    {
        private readonly int _turnNumber;
        private readonly Player _player;

        public int TurnNumber => _turnNumber;
        public string PlayerTag => _player.tag;
        public Player Player => _player;

        public TurnOptions(int turnNumber, Player player)
        {
            _turnNumber = turnNumber;
            _player = player;
        }
    }
}
