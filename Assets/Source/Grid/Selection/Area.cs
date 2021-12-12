using System.Collections.Generic;

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

            var center = group.Center.transform;
            transform.position = new Vector3(
                center.position.x,
                center.position.y + 0.1f,
                center.position.z
            );

            _mesh.Triangulate(group.Cells);
            _current.Cells.ForEach(cell => cell.Select());
        }
    }

}
