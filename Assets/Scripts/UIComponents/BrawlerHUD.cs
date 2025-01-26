using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.UIComponents
{
    public class BrawlerHUD : MonoBehaviour
    {
        private Brawler _brawler;
        public TextMeshProUGUI playerText;
        public Image healthBar;
        public Image staminaBar;
        private int maxHealth;
        private float maxStamina;
        
        public void SetBrawler(Brawler brawler)
        {
            _brawler = brawler;
            var healthComponent = _brawler.Get<HealthComponent>();
            healthComponent.OnHealthChanged += OnHealthChanged;
            maxHealth = healthComponent.MaxHealth;
            var staminaComponent = _brawler.Get<StaminaComponent>();
            staminaComponent.OnStaminaChanged += OnStaminaChanged;
            maxStamina = staminaComponent.MaxStamina;
            playerText.text = _brawler.name;
        }
        
        private void OnHealthChanged(int health)
        {
            healthBar.fillAmount = (float)health / maxHealth;
        }

        private void OnStaminaChanged(float stamina)
        {
            staminaBar.fillAmount = stamina / maxStamina;
        }
    }
}