using System;
using System.Collections.Generic;
using GGJ2025.AttackSystem;
using Karma.Physics;
using UnityEngine;

namespace GGJ2025
{
    public class Brawler : MonoBehaviour, IHittable
    {
        [field: SerializeField] public MeleeAttack MeleeAttack { get; private set; }
        [field: SerializeField] public ProjectileAttack ProjectileAttack { get; private set; }

        #region BrawlerComponents

        private readonly List<BaseBrawlerComponent> _brawlerComponents = new List<BaseBrawlerComponent>();
        private readonly Dictionary<Type, BaseBrawlerComponent> _brawlerComponentsDictionary = new();
        public event Action<BaseBrawlerComponent> OnComponentAdded;

        public T Get<T>() where T : BaseBrawlerComponent
        {
            if (_brawlerComponentsDictionary.ContainsKey(typeof(T)))
            {
                return (T)_brawlerComponentsDictionary[typeof(T)];
            }

            foreach (var brawlerComponent in _brawlerComponents)
            {
                if (brawlerComponent is not T t) continue;

                _brawlerComponentsDictionary.Add(typeof(T), t);
                return t;
            }

            return null;
        }

        public void RegisterBrawlerComponent<T>(T brawlerComponent) where T : BaseBrawlerComponent
        {
            brawlerComponent.SetBrawler(this);
            _brawlerComponents.Add(brawlerComponent);
            OnComponentAdded?.Invoke(brawlerComponent);
        }

        #endregion

        [field: SerializeField] public List<GameObject> IgnoredObjects { get; private set; } = new ();
        [field: SerializeField] public BrawlerInputHandler InputHandler { get; private set; }
        [field: SerializeField] public Transform AttackPoint { get; private set; }
        [field: SerializeField] public Transform ParryPoint { get; private set; }
        [field: SerializeField] public Transform BalloonPoint { get; private set; }
        [field: SerializeField] public Transform GroundCheck { get; private set; }
        [field: SerializeField] public Collider2D PickupCollider { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Collider2D BodyCollider { get; private set; }

        private void Awake()
        {
            IgnoredObjects.Add(gameObject);
            IgnoredObjects.Add(BodyCollider.gameObject);
        }

        public void SetInputHandler(BrawlerInputHandler brawlerInputHandler)
        {
            InputHandler = brawlerInputHandler;
        }
        public void OnHit(HitInfo hitInfo)
        { 
            foreach (var brawlerComponent in _brawlerComponents)
            {
                brawlerComponent.OnHit(hitInfo);
            }
        }
        
    }
}