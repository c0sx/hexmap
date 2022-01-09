using UnityEngine;

namespace Map.Cell.State
{
    public class Selected: IState
    {
        public Selected(Material material, Renderer renderer) 
        {
            renderer.material = material;
        }

        public bool IsClickable(GridCell cell) 
        {
            return true;
        }
    }
}

