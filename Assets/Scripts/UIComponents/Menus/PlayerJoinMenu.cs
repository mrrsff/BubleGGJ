using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace GGJ2025.UIComponents.Menus
{
    public class PlayerJoinMenu : MonoBehaviour
    {
        private List<PlayerDescription> _playerDescriptions = new ();
        public MapSelector MapSelector;
        public ModeSelector ModeSelector;

        private int _playerCount = 0;
        private void Awake()
        {
            _playerDescriptions = GetComponentsInChildren<PlayerDescription>(true).ToList();
        }

        private void OnEnable()
        {
            _playerCount = 0;
            _playerDescriptions.ForEach(playerDescription => playerDescription.gameObject.SetActive(false));
            GameManager.Instance.OnPlayerJoinedEvent += OnPlayerJoin;
            var players = GameManager.Instance.GetPlayers();
            foreach (var player in players)
            {
                OnPlayerJoin(player);
            }
        }

        private void OnDisable()
        {
            GameManager.Instance.OnPlayerJoinedEvent -= OnPlayerJoin;
        }

        private void OnPlayerJoin(PlayerInputHandler playerInputHandler)
        {
            if (_playerCount >= _playerDescriptions.Count) return;
            var description = _playerDescriptions[_playerCount];
            description.gameObject.SetActive(true);
            description.SetPlayerDescription(playerInputHandler);
            _playerCount++;
        }
        
        public List<Brawler> GetPlayerSelections()
        {
            var playerSelections = new List<Brawler>();
            for (var i = 0; i < _playerCount; i++)
            {
                playerSelections.Add(_playerDescriptions[i].BrawlerSelector.SelectedObject);
            }
            return playerSelections;
        }
    }
}