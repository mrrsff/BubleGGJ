using System.Collections.Generic;
using Karma;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        private PlayerInputManager playerInputManager;
        private List<PlayerInputHandler> players = new ();

        private BrawlManager brawlManager;
        private void Awake()
        {
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
        } 
        
        public void EnableJoining()
        {
            playerInputManager.EnableJoining();
        }
        
        public void DisableJoining()
        {
            playerInputManager.DisableJoining();
        }
        
        public void BasicStartBrawl()
        {
            brawlManager = BrawlManager.StartBrawl(players, GameResources.BasicBrawlMap, GameResources.BasicBrawlMode);
            DisableJoining();
        }

        public void StartBrawl(BrawlMap selectedMap, BrawlMode selectedMode)
        {
            brawlManager = BrawlManager.StartBrawl(players, selectedMap, selectedMode);
            DisableJoining();
        }
    }
}