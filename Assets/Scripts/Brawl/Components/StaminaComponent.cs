using System;
using System.Collections.Generic;
using GGJ2025.Interfaces;
using Karma;
using UnityEngine;

namespace GGJ2025
{
    public class StaminaComponent : BaseBrawlerComponent, IComponentEffector<MovementComponent, float>
    {
        [SerializeField] private float currentStamina;
        private float maxStamina = 100;
        private float staminaRegenRate = 35f;
        private Cooldown staminaRegenCooldown = new Cooldown(0.1f);
        private Cooldown staminaRegenDelay = new Cooldown(.5f);
        private float jumpStaminaCost = 5;
        private MovementComponent movementComponent;
        
        public float MaxStamina => maxStamina;
        public event Action<float> OnStaminaChanged; 
        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            movementComponent = brawler.Get<MovementComponent>();
            movementComponent.OnJumpEvent += () => UseStamina(jumpStaminaCost);
            currentStamina = maxStamina;
        }

        private void Update()
        {
            if (staminaRegenCooldown.IsReady && staminaRegenDelay.IsReady)
            {
                RegenStamina();
            }

            movementComponent.canJump = CanUseStamina(jumpStaminaCost);
        }
        public bool CanUseStamina(float amount)
        {
            return currentStamina >= amount;
        }
        public void UseStamina(float amount)
        {
            currentStamina = Mathf.Clamp(currentStamina - amount, 0, maxStamina);
            staminaRegenCooldown.Reset();
            staminaRegenDelay.Reset();
            OnStaminaChanged?.Invoke(currentStamina);
        }
        
        private void RegenStamina()
        {
            var newStamina = currentStamina +
                             (staminaRegenRate - 5 * DetermineStaminaState()) * staminaRegenCooldown.duration;
            currentStamina = Mathf.Clamp(newStamina, 0, maxStamina);
            staminaRegenCooldown.Reset();
            OnStaminaChanged?.Invoke(currentStamina);
        }
        
        private int DetermineStaminaState()
        {
            // 20-40-40 => 15-20-25
            var percentage = currentStamina / maxStamina;
            return percentage switch
            {
                < 0.2f => 1,
                < 0.4f => 2,
                _ => 3
            };
        }
        
        public List<int> effectIds { get; set; } = new();
        public int ApplyEffect(float effect)
        {
            int id = Brawler.Get<MovementComponent>().AddEffect(effect);
            effectIds.Add(id);
            return id;
        }

        public void RemoveEffect(int Id)
        {
            Brawler.Get<MovementComponent>().RemoveEffect(Id);
        }
    }
}