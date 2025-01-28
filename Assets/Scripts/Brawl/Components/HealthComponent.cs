using System;
using UnityEngine;

namespace GGJ2025
{
    public class HealthComponent : BaseBrawlerComponent
    {
        [field: SerializeField] public int Health { get; private set; } = 5;
        [field: SerializeField] public int MaxHealth { get; private set; } = 5;

        private bool isDead;
        public event Action OnDeath;
        public event Action<int> OnHealthChanged;
        public override void OnHit(HitInfo hitInfo)
        {
            if (isDead && hitInfo.Damage == 0) return;
            Brawler.audioSource.PlayOneShot(Brawler.hitSound);
            ChangeHealth(-hitInfo.Damage);
            if (Health <= 0)
            {
                Die();
            }
        }
        
        public void ChangeHealth(int amount)
        {
            Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
            OnHealthChanged?.Invoke(Health);
        }

        private void Die()
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }
}