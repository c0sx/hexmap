using System;

using UnityEngine;

namespace Grid 
{
    [RequireComponent(typeof(HexCellMesh))]
    public class HexCell : MonoBehaviour
    {
        public Action<HexCell> Clicked;

        [SerializeField] private HexCoordinates _coordinates;
        [SerializeField] private Pawn _pawnPrefab;
        [SerializeField] private Color _current;
        [SerializeField] private Color _previous;
        private HexCellMesh _mesh;
        private HexMetrics _metrics;
        private Pawn _pawn;
        
        private void OnMouseDown()
        {
            Clicked?.Invoke(this);
        }

        public void Init(HexCoordinates coordinates, HexMetrics metrics)
        {
            _coordinates = coordinates;
            _metrics = metrics;

            _mesh = GetComponent<HexCellMesh>();
        }

        public void Triangulate()
        {
            _mesh.Triangulate(_metrics, this);
        }

        public void Highlight()
        {
            (_previous, _current) = (_current, _previous);
            _mesh.Color(_current);
        }

        public void PlacePawn(Pawn pawn)
        {
            _pawn = pawn;

            _pawn.transform.SetParent(transform);
            _pawn.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        public override string ToString()
        {
            return _coordinates.ToString();
        }
    }

}

