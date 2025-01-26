using System.Collections;
using System.Collections.Generic;
using Karma.Pooling;
using UnityEngine;

namespace GGJ2025.AttackSystem
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "GGJ2025/AttackSystem/ProjectileAttack")]
    public class ProjectileAttack : Attack
    {
        public float ProjectileSpeed = 10f;
        protected override void ExecuteAttack(Brawler attacker, List<GameObject> ignoredObjects)
        {
            var projectile = ObjectPooler.DequeueObject<Projectile>(GameResources.GameSettings.ProjectilePoolKey);
            projectile.transform.position = attacker.BalloonPoint.position + attacker.transform.right * (attacker.transform.lossyScale.x * projectile.transform.lossyScale.x);    
            projectile.transform.rotation = Quaternion.identity;
            projectile.gameObject.SetActive(true);
            projectile.OnHit += (coll) => OnProjectileHit(projectile, attacker, coll);
            projectile.Rigidbody.linearVelocityX = attacker.transform.right.x * ProjectileSpeed * (ChargeAmount + 1) *
                                                  attacker.transform.localScale.x;
            
            // Timer to deactivate the projectile
            attacker.StartCoroutine(DeactivateProjectileAfterTime(projectile));
        }
        private IEnumerator DeactivateProjectileAfterTime(Projectile projectile)
        {
            yield return new WaitForSeconds(5);
            ObjectPooler.EnqueueObject(projectile, GameResources.GameSettings.ProjectilePoolKey);
        }
        
        private void OnProjectileHit(Projectile projectile, Brawler attacker, Collision2D collision)
        {
            var hittable = collision.gameObject.GetComponentInParent<IHittable>();
            if (hittable == null)
            {
                return;
            }
            var hitInfo = new HitInfo
            {
                Source = attacker.gameObject,
                Direction = collision.contacts[0].normal,
                Force = Force * (ChargeAmount + 1)
            };
            hittable.OnHit(hitInfo);
            
            Debug.Log("Projectile hit: " + collision.gameObject.name);
            ObjectPooler.EnqueueObject(projectile, GameResources.GameSettings.ProjectilePoolKey);
        }
    }
}