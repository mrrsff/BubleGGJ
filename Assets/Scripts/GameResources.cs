using GGJ2025.AttackSystem;
using Karma.Pooling;
using UnityEngine;

namespace GGJ2025
{
    public static class GameResources
    {
        public static readonly Brawler Brawler;
        public static readonly GameSettings GameSettings;
        public static GameObject IndicatorPrefab;
        public static BrawlMode BasicBrawlMode;
        public static BrawlMap BasicBrawlMap;
        public static Projectile ProjectilePrefab;

        static GameResources()
        {
            Brawler = Resources.Load<Brawler>("Brawler");
            GameSettings = Resources.Load<GameSettings>("GameSettings");
            BasicBrawlMode = Resources.Load<BrawlMode>("BasicBrawl");
            BasicBrawlMap = Resources.Load<BrawlMap>("BasicBrawlMap");
            ProjectilePrefab = Resources.Load<Projectile>("Projectile");
            IndicatorPrefab = Resources.Load<GameObject>("Indicator");
            if (Application.isPlaying) ObjectPooler.SetupPool(ProjectilePrefab, 10, GameSettings.ProjectilePoolKey);
        }
    }
}