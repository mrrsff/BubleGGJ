using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025.AttackSystem
{
    public abstract class Attack : ScriptableObject
    {
        public float Force;
        public float MinChargeTime;
        public float MaxChargeTime;
        public bool IsCharging { get; private set; }

        private Brawler _brawler;
        private List<GameObject> _ignoredObjects;
        private float chargeStartTime;

        public float ChargeAmount; // Normalized value between 0 and 1
        public event Action OnChargeStart;
        public event Action OnChargeEnd;
        public virtual void StartCharge(Brawler brawler, List<GameObject> ignoredObjects)
        {
            OnChargeStart?.Invoke();
            IsCharging = true;
            _brawler = brawler;
            _ignoredObjects = ignoredObjects;
            chargeStartTime = Time.time;
            ChargeAmount = 0;
        }
        
        public void UpdateCharge()
        {
            if (!IsCharging) return;
            ChargeAmount = Mathf.Clamp01((Time.time - chargeStartTime) / MaxChargeTime);
            if (ChargeAmount >= 1)
            {
                EndCharge();
            }
        }

        public virtual void EndCharge()
        {
            OnChargeEnd?.Invoke();
            IsCharging = false;
            if (Time.time - chargeStartTime < MinChargeTime) return;
            
            ChargeAmount = Mathf.Clamp01((Time.time - chargeStartTime) / MaxChargeTime);
            ExecuteAttack(_brawler, _ignoredObjects);
        }

        protected abstract void ExecuteAttack(Brawler attacker, List<GameObject> ignoredObjects);
    }
}