using UnityEngine;

namespace Map.Cell.State
{
    public class Default: IState
    {
        public Default(Material material, Renderer renderer)
        {
            renderer.material = material;
        }

        public bool IsClickable(GridCell cell) 
        {
            return cell.Occupied;
        }
    }
}

