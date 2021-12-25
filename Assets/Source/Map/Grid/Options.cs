using UnityEngine;

namespace Map.Grid
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private float _outerRadius = 10f;
        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 10;
        [SerializeField] private float _border = 0.5f;

        public int Width => _width;
        public int Height => _height;
        public int GridSize => _width * _height;
        public float OuterRadius => _outerRadius;
        public float Border => _border;
    }
}

