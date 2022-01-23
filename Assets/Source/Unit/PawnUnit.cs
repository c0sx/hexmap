using System;
using System.Collections.Generic;

using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer), typeof(Unit))]
    public class PawnUnit : MonoBehaviour
    {
        public event Action<PawnUnit> Selected;
        
        private Unit _unit;
        
        private MeshRenderer _mesh;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
        }
        
        private void OnMouseDown()
        {
            Selected?.Invoke(this);
        }

        public void Init(Unit unit, Transform target)
        {
            _unit = unit;
            
            PlaceTo(target);
        }

        public List<Vector2Int> GetMovingAxes()
        {
            return _unit.GetMovingAxes();
        }

        public List<Vector2Int> GetLookingAroundAxes()
        {
            return _unit.GetLookingAroundAxes();
        }

        private void PlaceTo(Transform other)
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