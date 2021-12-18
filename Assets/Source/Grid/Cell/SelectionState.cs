using UnityEngine;

namespace Grid.Cell 
{
    public class SelectionState : MonoBehaviour
    {
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _selectedMaterial;

        public void Init(Renderer renderer) 
        {
            renderer.material = _defaultMaterial;
        }
        
        public void Select(Renderer renderer)
        {
            renderer.material = _selectedMaterial;
        }

        public void Deselect(Renderer renderer)
        {
            renderer.material = _defaultMaterial;
        }
    }
}

