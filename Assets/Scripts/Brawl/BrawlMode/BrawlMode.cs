using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025
{
    public abstract class BrawlMode : ScriptableObject
    {
        protected BrawlManager brawlManager { get; private set; }
        protected virtual List<Type> necessaryComponents { get; } = new List<Type>();
        protected List<Brawler> brawlers;

        private void SetupBrawler(Brawler brawler)
        {
            foreach (var component in necessaryComponents)
            {
                var newComponent = brawler.gameObject.AddComponent(component) as BaseBrawlerComponent;
                brawler.RegisterBrawlerComponent(newComponent);
            }
        }

        public virtual void OnBrawlStart(BrawlManager brawlManager)
        {
            this.brawlManager = brawlManager;
            brawlers = brawlManager.players;
            foreach (var brawler in brawlers)
            {
                SetupBrawler(brawler);
            }
        }
        public abstract void OnBrawlEnd(BrawlManager brawlManager);
    }
}
//babacik patatesi gordu ama ne dedi biliyomusun ne yasaniyo kimse anlamiyo ne yasaniyo burda bu oyunda buble yok ne yapcaz bunun okuyanin da ben taaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa tatli veriyim ?D