using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction MoveAction;
    public Vector2 MoveInput { get; private set; }
    
    public event Action<bool> OnJumpEvent;
    public event Action<bool>  OnNormalAttackEvent;
    public event Action<bool>  OnBalloonAttackEvent;
    public event Action OnParryEvent; 
    
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
    private void Update()
    {
        MoveInput = MoveAction.ReadValue<Vector2>();
    }
    private void OnJump(InputAction.CallbackContext context) => OnJumpEvent?.Invoke(context.ReadValueAsButton());
    private void OnNormalAttack(InputAction.CallbackContext context) => OnNormalAttackEvent?.Invoke(context.ReadValueAsButton());
    private void OnBalloonAttack(InputAction.CallbackContext obj) => OnBalloonAttackEvent?.Invoke(obj.ReadValueAsButton());
    private void OnParry(InputAction.CallbackContext obj) => OnParryEvent?.Invoke();
}
