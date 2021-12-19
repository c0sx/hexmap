using UnityEngine;

namespace Grid.Cell.State
{
    public class Default: IState
    {
        private Material _material;

        public Material Material() => _material;

        public Default(Material material, Renderer renderer)
        {
            _material = material;
            renderer.material = material;
        }

        public bool IsClickable(GridCell cell) 
        {
            return cell.Occupied;
        }
    }
}

