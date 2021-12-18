using UnityEngine;

namespace Grid.Cell
{
    public class SelectionState: MonoBehaviour
    {
        [SerializeField] private Material _default;
        [SerializeField] private Material _selected;

        private State _current;
        private Renderer _renderer;

        public void Init(HexCell cell)
        {
            _renderer = cell.MeshRenderer;
            _current = new Default(_default, _renderer);
        }

        public void Select()
        {
            _current = new Selected(_selected, _renderer);
        }

        public void Deselect()
        {
            _current = new Default(_default, _renderer);
        }

        public bool IsClickable(HexCell cell) 
        {
            return _current.IsClickable(cell);
        }        
    }
}

