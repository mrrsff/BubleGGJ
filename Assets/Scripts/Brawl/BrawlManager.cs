using System.Collections.Generic;
using System.Linq;
using Karma;
using Karma.Extensions;
using UnityEngine;

namespace GGJ2025
{
    public class BrawlManager : MonoBehaviour
    {
        public static BrawlManager StartBrawl(List<PlayerInputHandler> playerInputHandlers, BrawlMap map, BrawlMode mode)
        {
            var brawlManager = new GameObject("BrawlManager").AddComponent<BrawlManager>();
            brawlManager.players = new List<Brawler>();
            brawlManager.map = Instantiate(map);
            var spawnPoints = brawlManager.map.spawnPoints.ToList();
            spawnPoints.Shuffle();
            for (var i = 0; i < playerInputHandlers.Count; i++)
            {
                var player = Instantiate(GameResources.Brawler, spawnPoints[i].position, Quaternion.identity, brawlManager.map.transform);
                player.gameObject.name = $"Player {i + 1}";
                player.SetInputHandler(playerInputHandlers[i]);
                brawlManager.players.Add(player);
            }
            mode.OnBrawlStart(brawlManager);
            return brawlManager;
        }
        public void EndBrawl(Brawler winner)
        {
            Debug.Log($"{winner.name} won the brawl!");
            Destroy(gameObject);
        }
        
        public List<Brawler> players { get; private set; } = new ();
        private BrawlMap map;
    }
}