using System;
using System.Collections.Generic;

using UnityEngine;

namespace Unit
{
    public class Queen: MonoBehaviour, Unit
    {
        public event Action<Unit, Unit> Eats;
        public event Action<Unit> Died;
        public event Action<Unit> Moved;
        
        [SerializeField] private int _distance;
        
        private Position _position;
        private QueenDirection _direction;

        private void Awake()
        {
            _direction = new QueenDirection();
        }

        public void Init(PawnUnit unit)
        {
            _position = unit.GetPosition();
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

