using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025.AttackSystem
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "GGJ2025/AttackSystem/MeleeAttack")]
    public class MeleeAttack : Attack
    {
        public float AttackRadius;
        protected override void ExecuteAttack(Brawler attacker, List<GameObject> ignoredObjects)
        {
            var colliders = Physics2D.OverlapCircleAll(attacker.AttackPoint.position, AttackRadius,
                GameResources.GameSettings.HittableLayer);

            foreach (var collider in colliders)
            {
                if (ignoredObjects != null && ignoredObjects.Contains(collider.gameObject)) continue;
                var hittable = collider.gameObject.GetComponentInParent<IHittable>();
                if (hittable == null)
                {
                    continue;
                }
                var hitInfo = new HitInfo
                {
                    Source = attacker.gameObject,
                    Direction = (collider.transform.position - attacker.AttackPoint.position).normalized,
                    Force = Force * (ChargeAmount+ 1)
                };
                hittable.OnHit(hitInfo);
            }
        }
    }
}