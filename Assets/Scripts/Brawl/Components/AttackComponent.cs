using System;
using System.Collections.Generic;
using DG.Tweening;
using GGJ2025.AttackSystem;
using GGJ2025.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ2025
{
    public class AttackComponent : BaseBrawlerComponent, IComponentEffector<MovementComponent, float>
    {
        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            brawler.InputHandler.OnNormalAttackEvent += NormalAttack;
            brawler.InputHandler.OnBalloonAttackEvent += SpecialAttack;
            brawler.InputHandler.OnParryEvent += Parry;
            
            brawler.MeleeAttack.OnChargeStart += OnChargeStart;
            brawler.ProjectileAttack.OnChargeStart += OnChargeStart;
            brawler.MeleeAttack.OnChargeEnd += OnChargeEnd;
            brawler.ProjectileAttack.OnChargeEnd += OnChargeEnd;
            
            brawler.audioSource.clip = brawler.chargeSound;
        }
        private void OnChargeStart()
        {
            ApplyEffect(0.1f);
        }
        private void OnChargeEnd()
        {
            RemoveEffect(effectIds[0]);
        }
        private void Update()
        {
            if(Brawler.MeleeAttack.IsCharging)
            {
                Brawler.MeleeAttack.UpdateCharge();
                Brawler.Shake(Brawler.MeleeAttack.ChargeAmount);
                Brawler.audioSource.Play();
            }
            if (Brawler.ProjectileAttack.IsCharging)
            {
                Brawler.ProjectileAttack.UpdateCharge();
                Brawler.Shake(Brawler.ProjectileAttack.ChargeAmount);
                Brawler.audioSource.Stop();
            }
        }

        private void NormalAttack(bool value)
        {
            if (value)
            {
                Brawler.MeleeAttack.StartCharge(Brawler, Brawler.IgnoredObjects);
                Brawler.audioSource.Play();
            }
            else if (Brawler.MeleeAttack.IsCharging)
            {
                Brawler.MeleeAttack.EndCharge();
                Brawler.audioSource.Stop();
            }
        }
        private void SpecialAttack(bool value)
        {
            if (value)
            {
                Brawler.ProjectileAttack.StartCharge(Brawler, Brawler.IgnoredObjects);
            }
            else if (Brawler.ProjectileAttack.IsCharging)
            {
                Brawler.ProjectileAttack.EndCharge();
            }
        }
        private void Parry()
        {
        }
        public List<int> effectIds { get; set; } = new List<int>();
        public int ApplyEffect(float effect)
        {
            int id = Brawler.Get<MovementComponent>().AddEffect(effect);
            effectIds.Add(id);
            return id;
        }

        public void RemoveEffect(int Id)
        {
            Brawler.Get<MovementComponent>().RemoveEffect(Id);
            effectIds.Remove(Id);
        }
    }
}