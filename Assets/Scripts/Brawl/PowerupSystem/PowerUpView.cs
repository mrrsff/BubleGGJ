using UnityEngine;

namespace GGJ2025
{
    public class PowerUpView : MonoBehaviour, IPickupable
    {
        public BasePowerUp PowerUp;
        public void SetPowerUp(BasePowerUp powerUp)
        {
            PowerUp = powerUp;
        }

        public void OnPickup(ICollector collector)
        {
            PowerUp.OnPickup(collector);
        }
    }
}