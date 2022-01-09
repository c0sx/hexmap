using UnityEngine;

namespace Map.Cell
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Selection : MonoBehaviour
    {
        [SerializeField] private Material _default;
        [SerializeField] private Material _selected;
        [SerializeField] private Material _queen;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public bool IsSelected()
        {
            return _renderer.material == _selected;
        }

        public void Select()
        {
            _renderer.material = _selected;
        }

        public void Deselect(bool isQueen)
        {
            _renderer.material = isQueen ? _queen : _default;
        }
    }
}