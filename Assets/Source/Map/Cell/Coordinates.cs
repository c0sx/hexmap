using System;

using UnityEngine;

using Map.Grid;

namespace Map.Cell
{
    [Serializable]
    public class Coordinates
    {
        [SerializeField] private readonly int _x;
        [SerializeField] private readonly int _z;

        public int X => _x;
        public int Z => _z;
        public int Y => -X - Z;

        public static Coordinates FromOffsetCoordinates(int x, int z) 
        {
            return new Coordinates(x - z / 2, z);
        }

        public static Coordinates FromVector2(Vector2Int v) 
        {
            return new Coordinates(v.x, v.y);
        }

        public static Coordinates FromPosition(Vector3 position, Metrics metrics) {
            float x = position.x / (metrics.InnerRadius * 2f);
            float y = -x;

            float offset = position.z / (metrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x -y);

            if (iX + iY + iZ != 0) {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x -y - iZ);

                if (dX > dY && dX > dZ) {
                    iX = -iY - iZ;
                }
                else if (dZ > dY) {
                    iZ = -iX - iY;
                }
            }

            return new Coordinates(iX, iZ);
        }

        public Coordinates(int x, int z) {
            _x = x;
            _z = z;
        }   

        public Vector2Int AxisWith(Coordinates target)
        {
            var t = new Vector2(X, Z) - new Vector2(target.X, target.Z);
            var distance = new Vector2Int(X, Z) - new Vector2Int(target.X, target.Z);
            Debug.Log("distance " + distance);
            var normalized = Normalize(distance);
            Debug.Log("normalized " + normalized + " " + normalized.normalized + " " + t.normalized);

            return distance;
        }

        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(X, Z);
        }

        public bool IsEqual(Coordinates other)
        {
            return X == other.X && Z == other.Z && Y == other.Y;
        }

        public override string ToString()
        {
            return "(" + this.X + ";" + this.Y + ";" + this.Z + ")";
        }

        private Vector2 Normalize(Vector2Int v)
        {
            var length = (float)Math.Sqrt((v.x * v.x) + (v.y * v.y));
	        var factor = (1 / length);
	        var x = v.x * factor;
            var y = v.y * factor;
            
            return new Vector2(
                (int) x,
                (int) y
            );
        }
    }
}




