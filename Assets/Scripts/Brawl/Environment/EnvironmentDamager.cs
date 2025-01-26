using UnityEngine;

namespace GGJ2025.Environment
{
    public class EnvironmentDamager : MonoBehaviour
    {
        public float Force = 100f;
        public int Damage = 1;
        public void OnCollisionEnter2D(Collision2D collision2D)
        {
            var brawler = collision2D.gameObject.GetComponentInParent<Brawler>();
            if (brawler != null)
            {
                var hitInfo = new HitInfo
                {
                    Force = Force,
                    Damage = Damage,
                    Direction = (brawler.transform.position - transform.position).normalized,
                    Source = gameObject
                };
                brawler.OnHit(hitInfo);
            }
        }
    }
}