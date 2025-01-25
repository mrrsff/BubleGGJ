using UnityEngine;

namespace GGJ2025
{
    public class PlayerManager : Brawler
    {
        private PlayerInputHandler playerInputHandler;
        [SerializeField] private PlayerMovement playerMovement;
        private void OnValidate() => ValidateRefs();
        private void ValidateRefs()
        {  
            if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        }
        private void Awake()
        {
            ValidateRefs();
        }
        public void SetInputHandler(PlayerInputHandler playerInputHandler)
        {
            this.playerInputHandler = playerInputHandler;
            playerMovement.SetInputHandler(playerInputHandler);
            
            playerInputHandler.OnParryEvent += OnParry;
            playerInputHandler.OnNormalAttackEvent += OnNormalAttack;
            playerInputHandler.OnBalloonAttackEvent += OnBalloonAttack;
        }
    }
}