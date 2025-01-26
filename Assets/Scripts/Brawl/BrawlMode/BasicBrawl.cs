using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025
{
    [CreateAssetMenu(fileName = "BasicBrawl", menuName = "GGJ2025/Brawl/BasicBrawl")]
    public class BasicBrawl : BrawlMode
    {
        protected override List<Type> necessaryComponents => new()
        {
            typeof(HealthComponent),
            typeof(AttackComponent),
            typeof(CollectorComponent),
            typeof(ForceReceiverComponent),
            typeof(MovementComponent)
        };

        public override void OnBrawlStart(BrawlManager brawlManager)
        {
            base.OnBrawlStart(brawlManager);

            foreach (var brawler in brawlers)
            {
                brawler.Get<HealthComponent>().OnDeath += () => DeathListener(brawler);
            }
        }

        public override void OnBrawlEnd(BrawlManager brawlManager)
        {
            
        }
        
        private void DeathListener(Brawler brawler)
        {
            brawlers.Remove(brawler);
            if (brawlers.Count == 1)
            {
                brawlManager.EndBrawl(brawlers[0]);
            }
        }
    }
}