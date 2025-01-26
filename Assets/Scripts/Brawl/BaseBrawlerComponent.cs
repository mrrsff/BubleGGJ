using UnityEngine;

namespace GGJ2025
{
    public abstract class BaseBrawlerComponent : MonoBehaviour, IHittable
    {
        public Brawler Brawler { get; private set; }
        public virtual void SetBrawler(Brawler brawler)
        {
            Brawler = brawler;
        }
        public virtual void OnHit(HitInfo hitInfo) { }
    }
}