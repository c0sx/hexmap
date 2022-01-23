using System;
using System.Collections.Generic;

using UnityEngine;

namespace Unit
{
    public interface Unit
    {
        event Action<Unit, Unit> Eats;
        event Action<Unit> Died;
        event Action<Unit> Moved;
        
        void Die();
        Position GetPosition();
        int GetMovingDistance();
        List<Vector2Int> GetMovingAxes();
        List<Vector2Int> GetLookingAroundAxes();
    }
}