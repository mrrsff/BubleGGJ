using UnityEngine;

namespace GGJ2025
{
    public class HitInfo
    {
        public int Damage;
        public Vector3 Direction;
        public float Force;
        public GameObject Source;
    }
    
    public interface IHittable
    {
        public void OnHit(HitInfo hitInfo);
    }
}