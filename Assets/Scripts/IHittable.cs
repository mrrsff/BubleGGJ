using UnityEngine;

namespace GGJ2025
{
    public class HitInfo
    {
        public int Damage { get; }
        public Vector3 Direction { get; }
        public GameObject Source { get; }
        public HitInfo(int damage, Vector3 direction, GameObject source)
        {
            Damage = damage;
            Direction = direction;
            Source = source;
        }
    }
    
    public interface IHittable
    {
        public void OnHit(HitInfo hitInfo);
    }
}