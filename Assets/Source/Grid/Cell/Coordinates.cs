using System;

using UnityEngine;

namespace Grid.Cell
{
    [Serializable]
    public class Coordinates
    {
        [SerializeField] private readonly int _x;
        [SerializeField] private readonly int _z;

        public int X => _x;
        public int Z => _z;
        public int Y => -X - Z;

        public static Coordinates FromOffsetCoordinates(int x, int z) {
            return new Coordinates(x - z / 2, z);
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

        public override string ToString()
        {
            return "(" + this.X + ";" + this.Y + ";" + this.Z + ")";
        }
    }
}




