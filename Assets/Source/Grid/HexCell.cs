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
        [SerializeField] private Color _notSelected;
        [SerializeField] private Color _selected;
        private Color _current;
        private HexCellMesh _mesh;
        private HexMetrics _metrics;
        private Pawn _pawn;

        public Mesh Mesh => _mesh.Mesh;
        
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

        public void Select()
        {
            _current = _selected;
            _pawn?.Select();
        }

        public void Deselect()
        {
            _current = _notSelected;
            _pawn?.Deselect();
        }


        public void PlacePawn(Pawn pawn)
        {
            _pawn = pawn;

            _pawn.transform.SetParent(transform);
            _pawn.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }

        public bool HasPawn(Pawn pawn)
        {
            if (!_pawn) {
                return false;
            }

            return _pawn == pawn;
        }

        public override string ToString()
        {
            return _coordinates.ToString();
        }
    }

}

