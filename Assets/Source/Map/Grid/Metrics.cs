using UnityEngine;

namespace Map.Grid
{
    public class Metrics
    {
        private readonly Options _options;
        private readonly float _innerRadius;
        private readonly Vector3[] _corners;

        public Vector3[] Corners => _corners;
        public float OuterRadius => _options.OuterRadius;
        public float InnerRadius => _innerRadius;
        public int Width => _options.Width;
        public int Height => _options.Height;

        public Metrics(Options options)
        {
            _options = options;
            _innerRadius = GetInnerRadius(options.OuterRadius);
            _corners = CalculateCorners(options.OuterRadius, _innerRadius);
        }

        public Vector3 GetPositionFor(int x, int z) 
        {
            var border = _options.Border;
            var xPosition = (x + z * 0.5f - z / 2) * (_innerRadius * 2f + border);
            var yPosition = 0f;
            var zPosition = z * (_options.OuterRadius * 1.5f + border);

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

}

