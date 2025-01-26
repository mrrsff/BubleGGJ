using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using GGJ2025.UIComponents;
using Karma;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public static Camera MainCamera;
        private PlayerInputManager playerInputManager;
        private List<PlayerInputHandler> players = new ();
        [field: SerializeField] public UIManager UIManager { get; private set; }

        public event Action<PlayerInputHandler> OnPlayerJoinedEvent; 
        private BrawlManager brawlManager;
        private void Awake()
        {
            MainCamera = Camera.main;
            UIManager = FindFirstObjectByType<UIManager>();
            playerInputManager = GetComponent<PlayerInputManager>();
            playerInputManager.onPlayerJoined += OnPlayerJoined;
            playerInputManager.onPlayerLeft += OnPlayerLeft;
            EnableJoining();
        }

        private void OnPlayerLeft(PlayerInput obj)
        {
            players.Remove(obj.GetComponent<PlayerInputHandler>());
        }

        private void OnPlayerJoined(PlayerInput obj)
        {
            players.Add(obj.GetComponent<PlayerInputHandler>());
            OnPlayerJoinedEvent?.Invoke(obj.GetComponent<PlayerInputHandler>());
        } 
        public void ResetPlayers()
        {
            // Destroy all players in a safe way (not in the middle of a loop)
            players.ForEach(player => Destroy(player.gameObject, 0.1f));
        }
        public List<PlayerInputHandler> GetPlayers()
        {
            return players;
        }
        public void EnableJoining()
        {
            playerInputManager.EnableJoining();
        }
        
        public void DisableJoining()
        {
            playerInputManager.DisableJoining();
        }

        public void StartBrawl(List<Brawler> playerSelections, BrawlMap selectedMap, BrawlMode selectedMode)
        {
            brawlManager = BrawlManager.StartBrawl(players, playerSelections, selectedMap, selectedMode);
            DisableJoining();
        }
    }
}