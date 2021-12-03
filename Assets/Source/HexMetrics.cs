using UnityEngine;

public class HexMetrics
{
    private readonly float _outerRadius;
    private readonly float _innerRadius;
    private readonly float _border;
    private readonly Vector3[] _corners;

    public Vector3[] Corners => _corners;
    public float OuterRadius => _outerRadius;
    public float InnerRadius => _innerRadius;

    public HexMetrics(float outerRadius, float border)
    {
        _outerRadius = outerRadius;
        _innerRadius = GetInnerRadius(_outerRadius);
        _border = border;
        _corners = CalculateCorners(_outerRadius, _innerRadius);
    }

    public Vector3 GetPositionFor(int i, int x, int z) 
    {
        float xPosition = (x + z * 0.5f - z / 2) * (_innerRadius * 2f + _border);
        float yPosition = 0f;
        float zPosition = z * (_outerRadius * 1.5f + _border);

        return new Vector3(
            xPosition,
            yPosition,
            zPosition
        );
    }

    private float GetInnerRadius(float outerRadius) 
    {
        var factor = Mathf.Sqrt(3) / 2;
        return outerRadius * factor;
    }

    private Vector3[] CalculateCorners(float outerRadius, float innerRadius)
    {
        return new Vector3[] {
            new Vector3(0f, 0f, outerRadius),
            new Vector3(innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(0f, 0f, -outerRadius),
            new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(0f, 0f, outerRadius)
        };
    }

}
