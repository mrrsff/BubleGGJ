using System;
using Karma;
using Karma.Extensions;
using UnityEngine;

namespace GGJ2025
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInputHandler playerInputHandler;
        [SerializeField] private Rigidbody2D rb;
        
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private Cooldown jumpCooldown = new Cooldown(0.05f);
        [SerializeField] private float MaxHorizontalSpeed = 5f;
        [SerializeField] private float MaxVerticalSpeed = 5f;
        [SerializeField] private float groundDistance = 1f;
        [SerializeField] private float groundDrag = 5f;
        private Vector2 moveInput;
        private bool isJumping;
        private bool isGrounded;
        private void OnValidate() => ValidateRefs();
        private void ValidateRefs()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
        }
        private void Awake()
        {
            ValidateRefs();
        }
        public void SetInputHandler(PlayerInputHandler playerInputHandler)
        {
            this.playerInputHandler = playerInputHandler;
            playerInputHandler.OnJumpEvent += (value) => isJumping = value;
        }
        private void Update()
        {
            if (!playerInputHandler) return;
            
            moveInput = playerInputHandler.MoveInput;

            CheckGrounded();
            if (isGrounded && moveInput.sqrMagnitude > 0) HandleMovement();
            if (isJumping && jumpCooldown.IsReady) HandleJump();
        }
        private void HandleMovement()
        {
            var currentSpeed = rb.linearVelocity;
            var force = moveInput.With(y:0) * moveSpeed;
            currentSpeed += force;
            
            var x = Mathf.Clamp(currentSpeed.x, -MaxHorizontalSpeed, MaxHorizontalSpeed);
            var y = Mathf.Clamp(currentSpeed.y, -MaxVerticalSpeed, MaxVerticalSpeed);
            rb.linearVelocity = new Vector2(x, y);
        }
        private void HandleJump()
        {
            var currentSpeed = rb.linearVelocity;
            var force = Vector2.up * jumpForce + (moveInput.With(y: 0) * jumpForce);
            currentSpeed += force;
            
            var x = Mathf.Clamp(currentSpeed.x, -MaxHorizontalSpeed, MaxHorizontalSpeed);
            var y = Mathf.Clamp(currentSpeed.y, -MaxVerticalSpeed, MaxVerticalSpeed);
            rb.linearVelocity = new Vector2(x, y);
            jumpCooldown.Reset();
        }

        private void CheckGrounded()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDistance,
                GameResources.GameSettings.GroundLayer);
            rb.linearDamping = isGrounded ? groundDrag : 0;
        }
    }
}