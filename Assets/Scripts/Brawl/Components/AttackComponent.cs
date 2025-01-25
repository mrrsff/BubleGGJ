using UnityEngine;

namespace GGJ2025
{
    public class AttackComponent : BaseBrawlerComponent
    {
        public override void OnHit(HitInfo hitInfo) { }

        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            brawler.OnNormalAttackEvent += NormalAttack;
            brawler.OnSpecialAttackEvent += SpecialAttack;
            brawler.OnParryEvent += Parry;
        }
        
        private void NormalAttack(bool value)
        {
        }
        private void SpecialAttack(bool value)
        {
        }
        private void Parry()
        {
        }
    }
}