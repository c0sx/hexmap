using System;
using System.Collections.Generic;

using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer), typeof(Unit))]
    public class PawnUnit : MonoBehaviour
    {
        public event Action<PawnUnit> Selected;
        public int Direction => _direction;

        private Unit _unit;
        private int _direction;
        
        private MeshRenderer _mesh;
        private Color _notSelected;
        private Color _selected;
        
        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _unit = GetComponent<Unit>();
        }
        
        private void OnMouseDown()
        {
            Selected?.Invoke(this);
        }

        public void Init(Player player)
        {
            _notSelected = player.Primary;
            _selected = player.Selected;
            _direction = player.Direction;
            
            _unit.Init(this);
            
            _mesh.material.color = _notSelected;
        }

        public Position GetPosition()
        {
            var position = transform.position;
            return new Position(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
        }

        public List<Vector2Int> GetMovingAxes()
        {
            return _unit.GetMovingAxes();
        }

        public List<Vector2Int> GetLookingAroundAxes()
        {
            return _unit.GetLookingAroundAxes();
        }

        public void PlaceTo(Transform other)
        {
            var current = transform;
            current.SetParent(other);
            
            var otherPosition = other.position;
            current.position = new Vector3(
                otherPosition.x,
                1,
                otherPosition.z
            );
        }
    }
}