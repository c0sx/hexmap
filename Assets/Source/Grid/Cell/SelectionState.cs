using UnityEngine;

namespace Grid.Cell 
{
    public class SelectionState : MonoBehaviour
    {
        [SerializeField] private Material _default;
        [SerializeField] private Material _selected;

        public void Init(Renderer renderer) 
        {
            renderer.material = _default;
        }

        public void Select(Renderer renderer)
        {
            renderer.material = _selected;
        }

        public void Deselect(Renderer renderer)
        {
            renderer.material = _default;
        }
    }
}

