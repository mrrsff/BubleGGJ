using System;
using UnityEngine;

namespace GGJ2025.AttackSystem
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        public event Action<Collision2D> OnHit;
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnHit?.Invoke(other);
            OnHit = null;
        }
    }
}