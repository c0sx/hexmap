using UnityEngine;

namespace Hexmap.Map.Cell.State
{
    public interface IState
    {
        Material Material();
        bool IsClickable(GridCell cell);
    }

}
