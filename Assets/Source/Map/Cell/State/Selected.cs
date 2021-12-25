using UnityEngine;

namespace Map.Cell.State
{
    public class Selected: IState
    {
        private Material _material;

        public Material Material() => _material;

        public Selected(Material material, Renderer renderer) 
        {
            _material = material;
            renderer.material = _material;
        }

        public bool IsClickable(GridCell cell) 
        {
            return true;
        }
    }
}

