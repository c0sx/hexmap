using UnityEngine;

namespace Grid.Cell.State
{
    public class SelectionState: MonoBehaviour
    {
        [SerializeField] private Material _default;
        [SerializeField] private Material _selected;

        private IState _current;
        private Renderer _renderer;

        public void Init(GridCell cell)
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

        public bool IsClickable(GridCell cell) 
        {
            return _current.IsClickable(cell);
        }        
    }
}

