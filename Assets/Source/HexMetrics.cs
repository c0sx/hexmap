using System.Collections.Generic;
using UnityEngine;

public class HexMetrics
{
    private readonly float _outerRadius;
    private readonly float _innerRadius;
    private readonly Vector3[] _corners;

    public Vector3[] Corners => _corners;
    public float OuterRadius => _outerRadius;
    public float InnerRadius => _innerRadius;

    public HexMetrics(float outerRadius)
    {
        _outerRadius = outerRadius;
        _innerRadius = GetInnerRadius(_outerRadius);
        _corners = CalculateCorners(_outerRadius, _innerRadius);
    }

    public Vector3 GetPositionFor(int x, int z) 
    {
        return new Vector3(
            (x + z * 0.5f - z / 2) * _outerRadius * 3f,
            0f,
            z * _outerRadius
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
            new Vector3(-outerRadius / 2, 0f, outerRadius),
            new Vector3(outerRadius / 2, 0f, outerRadius),
            new Vector3(outerRadius, 0f, 0f),
            new Vector3(outerRadius / 2, 0f, -outerRadius),
            new Vector3(-outerRadius / 2, 0f, -outerRadius),
            new Vector3(-outerRadius, 0f, 0f),
            new Vector3(-outerRadius / 2, 0f, outerRadius),
        };
    }

}
