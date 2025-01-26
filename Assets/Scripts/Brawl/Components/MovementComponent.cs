using System;
using System.Collections.Generic;
using System.Linq;
using GGJ2025.Interfaces;
using Karma;
using Karma.Extensions;
using UnityEngine;

namespace GGJ2025
{
    public class MovementComponent : BaseBrawlerComponent, IEffectable<float>
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private Cooldown jumpCooldown = new (0.25f);
        [SerializeField] private float MaxHorizontalSpeed = 8f;
        [SerializeField] private float MaxVerticalSpeed = 8f;
        [SerializeField] private float groundDistance = 0.25f;
        [SerializeField] private float groundDrag = 5f;
        private Vector2 moveInput;
        private bool isJumping;
        private bool isGrounded;
        private Rigidbody2D rb;
        
        public event Action OnJumpEvent;
        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            rb = brawler.Rigidbody;
            
            Brawler.InputHandler.OnJumpEvent += (value) => isJumping = value;
        }

        private void Update()
        {
            if (Brawler == null) return;
            moveInput = Brawler.InputHandler.GetMoveInput();
            transform.localScale = moveInput.x switch
            {
                < 0 => new Vector3(-1, 1, 1),
                > 0 => new Vector3(1, 1, 1),
                _ => transform.localScale
            };

            CheckGrounded();
            if (isGrounded && moveInput.sqrMagnitude > 0) HandleMovement();
            if (isJumping && jumpCooldown.IsReady) HandleJump();
        }
        private void HandleMovement()
        {
            var currentSpeed = rb.linearVelocity;
            var force = moveInput.With(y:0) * moveSpeed;
            currentSpeed += force;

            var x = Mathf.Clamp(currentSpeed.x, -MaxHorizontalSpeed, MaxHorizontalSpeed) * GetMoveSpeedMultiplier();
            var y = Mathf.Clamp(currentSpeed.y, -MaxVerticalSpeed, MaxVerticalSpeed) * GetMoveSpeedMultiplier();
            rb.linearVelocity = new Vector2(x, y);
        }
        private void HandleJump()
        {
            var currentSpeed = rb.linearVelocity;
            var force = Vector2.up * jumpForce + (moveInput.With(y: 0) * jumpForce);
            currentSpeed += force * GetMoveSpeedMultiplier();
            
            var x = Mathf.Clamp(currentSpeed.x, -MaxHorizontalSpeed, MaxHorizontalSpeed);
            var y = Mathf.Clamp(currentSpeed.y, -MaxVerticalSpeed, MaxVerticalSpeed);
            rb.linearVelocity = new Vector2(x, y);
            jumpCooldown.Reset();
            
            OnJumpEvent?.Invoke();
        }

        private void CheckGrounded()
        {
            isGrounded = Physics2D.Raycast(Brawler.GroundCheck.position, Vector2.down, groundDistance,
                GameResources.GameSettings.GroundLayer);
            rb.linearDamping = isGrounded ? groundDrag : 0;
        }
        
        public List<(int, float)> Effects { get; set; } = new();
        private float lastCalculatedMult;
        private bool isDirty = true;
        public int AddEffect(float effect)
        {
            isDirty = true;
            var id = Effects.Count > 0 ? Effects.Max(x => x.Item1) + 1 : 0;
            Effects.Add((id, effect));
            return id;
        }

        public void RemoveEffect(int ID)
        {
            isDirty = true;
            Effects.Remove(Effects.Find(x => x.Item1 == ID));
        }

        private float GetMoveSpeedMultiplier()
        {
            if (isDirty)
            {
                isDirty = false;
                if (Effects == null || Effects.Count == 0)
                {
                    lastCalculatedMult = 1f;
                    return lastCalculatedMult;
                }
                lastCalculatedMult =  Effects.Aggregate(1f, (current, effect) => current * effect.Item2);
            }
            return lastCalculatedMult;
        }
    }
}