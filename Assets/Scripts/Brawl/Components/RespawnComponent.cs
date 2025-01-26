using System;
using UnityEngine;

namespace GGJ2025
{
    public class RespawnComponent : BaseBrawlerComponent
    {
        public float MaxDistance = 15f;

        private Vector3 startPos;
        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            startPos = brawler.transform.position;
        }

        private void Update()
        {
            if (Brawler == null) return;
            if (Vector3.Distance(Brawler.transform.position, startPos) > MaxDistance)
            {
                Brawler.transform.position = startPos;
                Brawler.Get<HealthComponent>().OnHit(new HitInfo(){Damage = 1});;
            }
        }
    }
}