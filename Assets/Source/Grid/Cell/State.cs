using UnityEngine;

namespace Grid.Cell
{
    public interface State
    {
        Material Material();
        bool IsClickable(HexCell cell);
    }

}
