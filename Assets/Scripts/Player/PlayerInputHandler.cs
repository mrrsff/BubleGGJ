using System;
using GGJ2025;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : BrawlerInputHandler
{
    private PlayerInput playerInput;
    private InputAction MoveAction;
    public Vector2 MoveInput { get; private set; }
    public int PlayerIndex;
    private void OnValidate() => ValidateRefs();
    private void ValidateRefs()
    {
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();
    }
    private void Awake()
    {
        ValidateRefs();
    }
    private void OnEnable()
    {
        MoveAction = playerInput.actions["Move"];
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Jump"].canceled += OnJump;
        
        playerInput.actions["Attack"].performed += OnNormalAttack;
        playerInput.actions["Attack"].canceled += OnNormalAttack;
        
        playerInput.actions["Balloon Attack"].performed += OnBalloonAttack;
        playerInput.actions["Balloon Attack"].canceled += OnBalloonAttack;
        
        playerInput.actions["Parry"].performed += OnParry;
    }
    private void OnDisable()
    {
        playerInput.actions["Jump"].performed -= OnJump;
        playerInput.actions["Jump"].canceled -= OnJump;
        
        playerInput.actions["Attack"].performed -= OnNormalAttack;
        playerInput.actions["Attack"].canceled -= OnNormalAttack;
        
        playerInput.actions["Balloon Attack"].performed -= OnBalloonAttack;
        playerInput.actions["Balloon Attack"].canceled -= OnNormalAttack;
        
        playerInput.actions["Parry"].performed -= OnParry;
    }

    public override Vector2 GetMoveInput() => MoveAction.ReadValue<Vector2>();
    private void OnJump(InputAction.CallbackContext context) => OnJump(context.ReadValueAsButton());
    private void OnNormalAttack(InputAction.CallbackContext context) => OnNormalAttack(context.ReadValueAsButton());
    private void OnBalloonAttack(InputAction.CallbackContext context) => OnBalloonAttack(context.ReadValueAsButton());
    private void OnParry(InputAction.CallbackContext context) => OnParry();
}
