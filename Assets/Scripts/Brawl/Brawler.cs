using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025
{
    public abstract class Brawler : MonoBehaviour, IHittable
    {
        public event Action<bool> OnNormalAttackEvent;
        public event Action<bool> OnSpecialAttackEvent;
        public event Action OnParryEvent; 
        
        [field: SerializeField] public Collider2D pickupCollider { get; private set; }
        public Transform transform { get; }
        private List<BaseBrawlerComponent> brawlerComponents = new List<BaseBrawlerComponent>();
        public void SetBrawlerComponent<T>(T brawlerComponent) where T : BaseBrawlerComponent
        {
            brawlerComponent.SetBrawler(this);
            brawlerComponents.Add(brawlerComponent);
        }
        public T Get<T>() where T : BaseBrawlerComponent
        { 
            return brawlerComponents.Find(x => x.GetType() == typeof(T)) as T;
        }
        public void OnHit(HitInfo hitInfo)
        { 
            foreach (var brawlerComponent in brawlerComponents)
            {
                brawlerComponent.OnHit(hitInfo);
            }
        }
        
        protected virtual void OnNormalAttack(bool value) => OnNormalAttackEvent?.Invoke(value);
        protected virtual void OnBalloonAttack(bool value) => OnSpecialAttackEvent?.Invoke(value);
        protected virtual void OnParry() => OnParryEvent?.Invoke();
    }
}