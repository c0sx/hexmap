using UnityEngine;

using Grid.Cell;

namespace Grid.Selection
{
    public class Area : MonoBehaviour
    {
        private Group _current;

        public void Select(Group group)
        {
            Clear();

            _current = group;

            transform.position = new Vector3(
                0,
                0.2f,
                0
            );

            group.Center.SelectPawn();
            group.Cells.ForEach(cell => cell.Select());
            Subscribe(group);
        }

        private void Subscribe(Group group)
        {
            var center = group.Center;
            var other = group.Cells.FindAll(cell => cell != center);

            foreach (var cell in other) {
                cell.Clicked += MovePawn;
            }
        }

        private void Unsubscribe(Group group)
        {
            var center = group.Center;
            var other = group.Cells.FindAll(cell => cell != center);

            foreach (var cell in other) {
                cell.Clicked -= MovePawn;
            }
        }

        private void MovePawn(HexCell to)
        {
            _current.Center.MovePawn(to);
            Clear();
        }

        private void Clear()
        {
            if (_current != null) {
                Unsubscribe(_current);
            }

            _current?.Cells.ForEach(cell => cell.Deselect());
        }
    }

}
