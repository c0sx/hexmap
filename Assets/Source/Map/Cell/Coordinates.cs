using UnityEngine;

using Map.Grid;

namespace Map.Cell
{
    public class Coordinates
    {
        private readonly int _x;
        private readonly int _z;

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
            var x = position.x / (metrics.InnerRadius * 2f);
            var y = -x;

            var offset = position.z / (metrics.OuterRadius * 3f);
            x -= offset;
            y -= offset;

            var iX = Mathf.RoundToInt(x);
            var iY = Mathf.RoundToInt(y);
            var iZ = Mathf.RoundToInt(-x -y);

            if (iX + iY + iZ != 0) {
                var dX = Mathf.Abs(x - iX);
                var dY = Mathf.Abs(y - iY);
                var dZ = Mathf.Abs(-x -y - iZ);

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
            return "(" + X + ";" + Y + ";" + Z + ")";
        }
    }
}




