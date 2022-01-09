using System.Collections.Generic;

using UnityEngine;

namespace Hexmap.Unit
{
    public class Direction
    {
        private readonly int _direction;
    
        public Direction(int direction)
        {
            _direction = direction;
        }
    
        public Vector2Int ForwardLeft()
        {
            return new Vector2Int(-1, 1) * _direction;
        }
    
        public Vector2Int ForwardRight()
        {
            return new Vector2Int(0, 1) * _direction;
        }
    
        public Vector2Int Left()
        {
            return new Vector2Int(-1, 0) * _direction;
        }
    
        public Vector2Int Right()
        {
            return new Vector2Int(1, 0) * _direction;
        }
    
        public Vector2Int BackwardLeft() 
        {
            return new Vector2Int(0, -1) * _direction;
        }
    
        public Vector2Int BackwardRight()
        {
            return new Vector2Int(1, -1) * _direction;
        }
    
        public List<Vector2Int> Forward()
        {
            return new List<Vector2Int> {
                ForwardLeft(),
                ForwardRight()
            };
        }
    
        public List<Vector2Int> Around() 
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
    }
}
