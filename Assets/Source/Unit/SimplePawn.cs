using System;
using System.Collections.Generic;

using UnityEngine;

namespace Unit
{
    public class SimplePawn: MonoBehaviour, Unit
    {
        public event Action<Unit, Unit> Eats;
        public event Action<Unit> Died;
        public event Action<Unit> Moved;

        [SerializeField] private int _distance;
        
        private Direction _direction;
        private Position _position;
        
        public void Init(PawnUnit unit)
        {
            _position = unit.GetPosition();
            _direction = new Direction(unit.Direction);
        }

        public Position GetPosition()
        {
            return _position;
        }
        
        public void Move(Position position)
        {
            _position = position;
            
            Moved?.Invoke(this);
        }
        
        public void Eat(Unit unit)
        {
            unit.Die();
            
            Eats?.Invoke(this, unit);
        }

        public void Die()
        {
            Died?.Invoke(this);
        }

        public int GetMovingDistance()
        {
            return _distance;
        }
        
        public List<Vector2Int> GetMovingAxes()
        {
            return _direction.Moving();
        }

        public List<Vector2Int> GetLookingAroundAxes()
        {
            return _direction.Looking();
        }
    }
}