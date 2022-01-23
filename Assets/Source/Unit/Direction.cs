using System.Collections.Generic;

using UnityEngine;

namespace Unit
{
    public class Direction
    {
        private readonly int _direction;

        public Direction(int direction)
        {
            _direction = direction;
        }

        public List<Vector2Int> Moving()
        {
            return new List<Vector2Int> {
                ForwardLeft(),
                ForwardRight()
            };
        }
    
        public List<Vector2Int> Looking() 
        {
            return new List<Vector2Int> {
                ForwardLeft(),
                ForwardRight(),
                Right(),
                BackwardRight(),
                BackwardLeft(),
                Left()
            };
        }

        private Vector2Int ForwardLeft()
        {
            return new Vector2Int(-1, 1) * _direction;
        }
    
        private Vector2Int ForwardRight()
        {
            return new Vector2Int(0, 1) * _direction;
        }
    
        private Vector2Int Left()
        {
            return new Vector2Int(-1, 0) * _direction;
        }
    
        private Vector2Int Right()
        {
            return new Vector2Int(1, 0) * _direction;
        }
    
        private Vector2Int BackwardLeft() 
        {
            return new Vector2Int(0, -1) * _direction;
        }
    
        private Vector2Int BackwardRight()
        {
            return new Vector2Int(1, -1) * _direction;
        }
    }
}
