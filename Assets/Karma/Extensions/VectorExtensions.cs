using UnityEngine;

namespace Karma.Extensions
{
    public static class VectorExtensions
    {
        // Vector3
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            => new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            => new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
        public static float DistanceXZ(Vector3 a, Vector3 b) => (b - a).With(y: 0).magnitude;
        public static float SqrMagnitudeXZ(Vector3 a, Vector3 b) => (b - a).With(y: 0).sqrMagnitude;
        public static Vector3 ToDirection(this Vector3 from, Vector3 to) => (to - from).normalized;
        public static float Magnitude(this Vector3 a, Vector3 b) => (b - a).magnitude;
        
        // Vector2
        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
            => new Vector2(x ?? vector.x, y ?? vector.y);
        public static Vector2 Add(this Vector2 vector, float? x = null, float? y = null)
            => new Vector2(vector.x + (x ?? 0), vector.y + (y ?? 0));
        
    }
}