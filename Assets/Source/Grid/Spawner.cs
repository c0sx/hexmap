using UnityEngine;

using Unit;

namespace Grid
{
    [RequireComponent(typeof(Player))]
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _size = 2;
        [SerializeField] private Pawn _pawnPrefab;

        private Player _player;

        public int Size => _size;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        public Pawn Spawn()
        {
            var pawn = Instantiate<Pawn>(_pawnPrefab);
            _player.Add(pawn);

            return pawn;
        }
    }
}


