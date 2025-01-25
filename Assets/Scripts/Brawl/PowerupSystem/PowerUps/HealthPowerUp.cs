using UnityEngine;

namespace GGJ2025.PowerUps
{
    [CreateAssetMenu(fileName = "HealthPowerUp", menuName = "GGJ2025/PowerUps/HealthPowerUp")]
    public class HealthPowerUp : PowerUp<HealthComponent>
    {
        [SerializeField] private int HealthAmount = 10;
        public override void OnPickup(ICollector collector)
        {
            var component = collector as BaseBrawlerComponent;
            var brawler = component?.Brawler;
            if (brawler == null) return;
            
            var healthComponent = brawler.Get<HealthComponent>();
            healthComponent?.ChangeHealth(HealthAmount);
        }
    }
}