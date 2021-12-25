using UnityEngine;

namespace Map.Cell.State
{
    public interface IState
    {
        Material Material();
        bool IsClickable(GridCell cell);
    }

}
