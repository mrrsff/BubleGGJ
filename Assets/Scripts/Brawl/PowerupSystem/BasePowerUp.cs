using UnityEngine;

namespace GGJ2025
{
    public abstract class BasePowerUp : ScriptableObject, IPickupable
    {
        public abstract void OnPickup(ICollector collector);
    }
}