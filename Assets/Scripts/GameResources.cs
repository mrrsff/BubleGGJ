using GGJ2025.AttackSystem;
using Karma.Pooling;
using UnityEngine;

namespace GGJ2025
{
    public static class GameResources
    {
        public static readonly GameSettings GameSettings;
        public static GameObject IndicatorPrefab;
        public static GameObject PlayerNamePrefab;
        public static GameObject BubblePrefab;
        public static Projectile ProjectilePrefab;

        static GameResources()
        {
            GameSettings = Resources.Load<GameSettings>("GameSettings");
            ProjectilePrefab = Resources.Load<Projectile>("Projectile");
            IndicatorPrefab = Resources.Load<GameObject>("Indicator");
            BubblePrefab = Resources.Load<GameObject>("Bubble");
            PlayerNamePrefab = Resources.Load<GameObject>("PlayerName");
            if (Application.isPlaying)
            {
                ObjectPooler.SetupPool(ProjectilePrefab, 10, GameSettings.ProjectilePoolKey);
                ObjectPooler.SetupPool(BubblePrefab, 20, GameSettings.BubblePoolKey);
            }
        }
    }
}