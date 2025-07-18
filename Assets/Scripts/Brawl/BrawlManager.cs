﻿using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GGJ2025.UIComponents;
using Karma;
using Karma.Extensions;
using UnityEngine;

namespace GGJ2025
{
    public class BrawlManager : MonoBehaviour
    {
        public static BrawlManager StartBrawl(List<PlayerInputHandler> players, List<Brawler> playerSelections, BrawlMap map, BrawlMode mode)
        {
            var brawlManager = new GameObject("BrawlManager").AddComponent<BrawlManager>();
            brawlManager.players = new List<Brawler>();
            brawlManager.map = Instantiate(map);
            var spawnPoints = brawlManager.map.spawnPoints.ToList();
            spawnPoints.Shuffle();
            for (var i = 0; i < players.Count; i++)
            {
                var selectedBrawler = playerSelections[i];
                var player = Instantiate(selectedBrawler, spawnPoints[i].position, Quaternion.identity, brawlManager.map.transform);
                player.gameObject.name = $"Player {i + 1}";
                player.SetInputHandler(players[i]);
                brawlManager.players.Add(player);
            }
            mode.OnBrawlStart(brawlManager);
            UIManager.Instance.SetBrawlers(brawlManager.players.ToArray());
            return brawlManager;
        }
        public void EndBrawl(Brawler winner)
        {
            winner.transform.SetParent(null);
            winner.BodyCollider.gameObject.transform.LookAt(-GameManager.MainCamera.transform.forward);
            UIManager.Instance.ShowWinMenu(winner);
            AudioManager.Instance.PlayWinMusic();
            GameManager.Instance.MoveCameraTo(winner);
            Destroy(gameObject);
        }
        
        public List<Brawler> players { get; private set; } = new ();
        private BrawlMap map;
    }
}