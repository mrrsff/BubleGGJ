using UnityEngine;

namespace GGJ2025
{
    public static class GameResources
    {
        public static readonly PlayerManager PlayerManager;
        public static readonly GameSettings GameSettings;
        public static BrawlMode BasicBrawlMode;
        public static BrawlMap BasicBrawlMap;

        static GameResources()
        {
            PlayerManager = Resources.Load<PlayerManager>("Player");
            GameSettings = Resources.Load<GameSettings>("GameSettings");
            BasicBrawlMode = Resources.Load<BrawlMode>("BasicBrawl");
            BasicBrawlMap = Resources.Load<BrawlMap>("BasicBrawlMap");
        }
    }
}