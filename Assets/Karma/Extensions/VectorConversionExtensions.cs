using System.Runtime.CompilerServices;
using UnityEngine;

namespace Karma.Extensions
{
    public static class VectorConversionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XY(this Vector3 vector) => new Vector2(vector.x, vector.y);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XZ(this Vector3 vector) => new Vector2(vector.x, vector.z);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToXY(this Vector2 vector, float z = 0) => new Vector3(vector.x, vector.y, z);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToXZ(this Vector2 vector, float y = 0) => new Vector3(vector.x, y, vector.y);
    }
}