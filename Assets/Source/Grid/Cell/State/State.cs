using UnityEngine;

namespace Grid.Cell.State
{
    public interface IState
    {
        Material Material();
        bool IsClickable(GridCell cell);
    }

}
