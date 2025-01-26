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
        [SerializeField] private float moveSpeed = 0.5f;
        [SerializeField] private float jumpForce = 3f;
        [SerializeField] private Cooldown jumpCooldown = new (0.25f);
        [SerializeField] private float MaxHorizontalSpeed = 2f;
        [SerializeField] private float MaxVerticalSpeed = 3f;
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private float groundDrag = 5f;
        private Vector2 moveInput;
        private bool isJumping;
        private bool isGrounded;
        private Rigidbody2D rb;
        public bool canJump;
        public event Action OnJumpEvent;
        public override void SetBrawler(Brawler brawler)
        {
            base.SetBrawler(brawler);
            rb = brawler.Rigidbody;
            rb.gravityScale = transform.lossyScale.y;
            MaxHorizontalSpeed *= transform.lossyScale.x;
            MaxVerticalSpeed *= transform.lossyScale.y;
            moveSpeed *= transform.lossyScale.x;
            jumpForce *= transform.lossyScale.y;
            
            Brawler.InputHandler.OnJumpEvent += (value) => isJumping = value;
        }

        private void Update()
        {
            if (Brawler == null) return;
            moveInput = Brawler.InputHandler.GetMoveInput();
            transform.localScale = moveInput.x switch
            {
                < 0 => transform.localScale.With(x: -Mathf.Abs(transform.localScale.x)),
                > 0 => transform.localScale.With(x: Mathf.Abs(transform.localScale.x)),
                _ => transform.localScale
            };

            CheckGrounded();
            if (isGrounded && moveInput.sqrMagnitude > 0) HandleMovement();
            if (canJump && isJumping && jumpCooldown.IsReady) HandleJump();
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
            currentSpeed = force * GetMoveSpeedMultiplier();
            
            var x = Mathf.Clamp(currentSpeed.x, -MaxHorizontalSpeed, MaxHorizontalSpeed);
            var y = Mathf.Clamp(currentSpeed.y, -MaxVerticalSpeed, MaxVerticalSpeed);
            rb.linearVelocity = new Vector2(x, y);
            jumpCooldown.Reset();
            
            OnJumpEvent?.Invoke();
        }

        private void CheckGrounded()
        {
            var collider = Physics2D.OverlapCircle(Brawler.GroundCheck.position, groundDistance, GameResources.GameSettings.GroundLayer);
            isGrounded = collider != null;
            
            rb.linearDamping = isGrounded ? groundDrag : 0;
        }

        private void ApplyGravity()
        {
            if (isGrounded)
            {
                // Stick to the ground
                var groundPosition = Physics2D.OverlapCircle(Brawler.GroundCheck.position, groundDistance, GameResources.GameSettings.GroundLayer).transform.position;
                rb.position = Vector2.Lerp(rb.position, groundPosition, Time.deltaTime * 10);
            }
            rb.AddForce(Vector2.down * GameResources.GameSettings.Gravity * rb.mass * rb.gravityScale);
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