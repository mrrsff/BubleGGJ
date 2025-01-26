using System;
using UnityEngine;

namespace GGJ2025
{
    public class RespawnComponent : BaseBrawlerComponent
    {
        public float MaxDistance = 8f;

        private Vector3 startPos;
        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            startPos = brawler.transform.position;
        }

        private void Update()
        {
            if (Brawler == null) return;
            var distance = Vector2.Distance(Brawler.transform.position, startPos);
            if (distance > MaxDistance)
            {
                Brawler.transform.position = startPos;
                Brawler.Get<HealthComponent>().OnHit(new HitInfo(){Damage = 1});;
            }
        }
    }
}