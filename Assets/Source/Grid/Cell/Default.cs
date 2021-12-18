using UnityEngine;

namespace Grid.Cell
{
    public class Default: State
    {
        private Material _material;

        public Material Material() => _material;

        public Default(Material material, Renderer renderer)
        {
            _material = material;
            renderer.material = material;
        }

        public bool IsClickable(HexCell cell) 
        {
            return cell.Occupied;
        }
    }
}

