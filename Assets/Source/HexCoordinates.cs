using UnityEngine;

[System.Serializable]
public class HexCoordinates
{
    [SerializeField] private readonly int _x;
    [SerializeField] private readonly int _z;

    public int X => _x;
    public int Z => _z;
    public int Y => -X - Z;

    public static HexCoordinates FromOffsetCoordinates(int x, int z) {
        return new HexCoordinates(x - z / 2, z);
    }

    public static HexCoordinates FromPosition(Vector3 position, HexMetrics metrics) {
        float x = position.x / (metrics.InnerRadius * 2f);
		float y = -x;
		float offset = position.z / (metrics.OuterRadius * 3f);
		x -= offset;
		y -= offset;

		int iX = Mathf.RoundToInt(x);
		int iY = Mathf.RoundToInt(y);
		int iZ = Mathf.RoundToInt(-x -y);

		return new HexCoordinates(iX, iZ);
    }

    public HexCoordinates(int x, int z) {
        _x = x;
        _z = z;
    }

    public override string ToString()
    {
        return this.X + ":" + this.Y + ":" + this.Z;
    }
}


