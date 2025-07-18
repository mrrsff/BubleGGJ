﻿using UnityEngine;

namespace GGJ2025
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GGJ2025/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public LayerMask GroundLayer;
        public LayerMask HittableLayer;
        public LayerMask PickupLayer;
        public string ProjectilePoolKey = "ProjectilePool";
        public string BubblePoolKey = "BubblePool";
        public float Gravity = 9.81f;
    }
}