using UnityEngine;

namespace Karma.Raycast
{
    public static class RaycastUtils
    {
        public static Ray[] GetViewRays(Transform transform, int rayCount)
        {
            var rays = new Ray[rayCount];
            var direction = transform.forward;
            var startAngle = -180;
            var angleStep = 360 / (rayCount - 1);

            var basePosition = transform.position + Vector3.up * 0.5f;

            for (int i = 0; i < rayCount; i++)
            {
                var currentAngle = startAngle + angleStep * i;
                var rotation = Quaternion.Euler(0, currentAngle, 0);
                rays[i] = new Ray(basePosition, rotation * direction);
            }

            return rays;
        }
    }
}