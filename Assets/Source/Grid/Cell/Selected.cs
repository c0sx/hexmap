using UnityEngine;

namespace Grid.Cell
{
    public class Selected: State
    {
        private Material _material;

        public Material Material() => _material;

        public Selected(Material material, Renderer renderer) 
        {
            _material = material;
            renderer.material = _material;
        }

        public bool IsClickable(HexCell cell) 
        {
            return true;
        }
    }
}

