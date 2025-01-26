using UnityEngine;
using UnityEngine.Events;

namespace GGJ2025.Environment
{
    public class EnvironmentalObject : MonoBehaviour, IHittable
    {
        public UnityEvent<HitInfo> OnHitEvent;

        public void OnHit(HitInfo hitInfo)
        {
            OnHitEvent.Invoke(hitInfo);
        }
    }
}