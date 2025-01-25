using System;
using Karma.Physics;
using UnityEngine;

namespace GGJ2025
{
    public class CollectorComponent : BaseBrawlerComponent, ICollector
    {
        public Collision2DReporter CollisionReporter { get; private set; }

        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            CollisionReporter = Brawler.pickupCollider.gameObject.AddComponent<Collision2DReporter>();
            CollisionReporter.OnTriggerEnterEvent += OnTriggerEntered;
            
        }

        private void OnTriggerEntered(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IPickupable pickupable))
            {
                Collect(pickupable);
            }
        }
        public void Collect(IPickupable pickupable)
        {
            pickupable.OnPickup(this);
        }
    }
}