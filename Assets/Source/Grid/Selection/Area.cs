using UnityEngine;

namespace Grid.Selection
{
    public class Area : MonoBehaviour
    {
        private Group _current;

        public void Select(Group group)
        {
            _current?.Cells.ForEach(cell => cell.Deselect());
            _current = group;

            transform.position = new Vector3(
                0,
                0.2f,
                0
            );

            group.Center.SelectPawn();
            group.Cells.ForEach(cell => cell.Select());
        }
    }

}
