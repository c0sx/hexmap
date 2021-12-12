using System.Collections.Generic;

using UnityEngine;

namespace Grid.Selection
{
    [RequireComponent(typeof(AreaMesh))]
    public class Area : MonoBehaviour
    {
        [SerializeField] private Grid.HexCell _current;
        private AreaMesh _mesh;

        private void Start()
        {
            _mesh = GetComponent<AreaMesh>();
        }

        public void Select(HexMetrics metrics, HexCell cell)
        {
            _current = cell;
            transform.position = new Vector3(
                cell.transform.position.x,
                cell.transform.position.y + 0.1f,
                cell.transform.position.z
            );

            Highlight(metrics);
        }

        private void Highlight(HexMetrics metrics)
        {
            // calculate area around cell
            // generate mesh for it
            // show it
            _mesh.Triangulate(metrics);
        }
    }

}
