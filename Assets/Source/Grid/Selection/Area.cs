using UnityEngine;

namespace Grid.Selection
{
    [RequireComponent(typeof(AreaMesh))]
    public class Area : MonoBehaviour
    {
        private Group _current;
        private AreaMesh _mesh;

        private void Start()
        {
            _mesh = GetComponent<AreaMesh>();
        }

        public void Select(Group group)
        {
            _current?.Cells.ForEach(cell => cell.Deselect());
            _current = group;

            transform.position = new Vector3(
                0,
                0.2f,
                0
            );

            // _mesh.Triangulate(group.Cells);
            group.Center.SelectPawn();
            group.Cells.ForEach(cell => cell.Select());
        }
    }

}
