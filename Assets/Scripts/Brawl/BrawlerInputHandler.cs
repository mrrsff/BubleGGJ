using System;
using UnityEngine;

namespace GGJ2025
{
    public class BrawlerInputHandler : MonoBehaviour
    {
        public event Action<bool> OnJumpEvent;
        public event Action<bool>  OnNormalAttackEvent;
        public event Action<bool>  OnBalloonAttackEvent;
        public event Action OnParryEvent;

        public virtual Vector2 GetMoveInput()
        {
            return Vector2.up;
        }
        protected void OnJump(bool isPressed) => OnJumpEvent?.Invoke(isPressed);
        protected void OnNormalAttack(bool isPressed) => OnNormalAttackEvent?.Invoke(isPressed);
        protected void OnBalloonAttack(bool isPressed) => OnBalloonAttackEvent?.Invoke(isPressed);
        protected void OnParry() => OnParryEvent?.Invoke();
        
    }
}