using System.Collections.Generic;
using UnityEngine;

public class HexMetrics
{
    private float _outerRadius = 10f;
    private float _innerRadius;
    private Vector3[] _corners;

    public HexMetrics(float outerRadius)
    {
        _outerRadius = outerRadius;
        _innerRadius = getInnerRadius(_outerRadius);
        _corners = getCorners(_outerRadius, _innerRadius);
    }

    public Vector3 GetPositionFor(int x, int z) 
    {
        return new Vector3(
            (x + z * 0.5f - z / 2) * _outerRadius * 3f,
            0f,
            z * _outerRadius
        );
    }

    public Vector3[] GetCorners()
    {
        return _corners;
    }

    private float getInnerRadius(float outerRadius) 
    {
        var factor = Mathf.Sqrt(3) / 2;
        return outerRadius * factor;
    }

    private Vector3[] getCorners(float outerRadius, float innerRadius)
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
