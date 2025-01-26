using System;
using System.Collections.Generic;
using Karma;
using Karma.Pooling;
using UnityEngine;

namespace GGJ2025
{
    public class BubbleSpawner : MonoBehaviour
    {
        public float maxY = 30f;
        public Cooldown bubbleSpawnRate = new Cooldown(2f);
        
        private List<GameObject> bubbles = new List<GameObject>();

        private void Start()
        {
            bubbleSpawnRate.StartCooldown(UnityEngine.Random.Range(0f, 2f));
        }

        private void SpawnBubble()
        {
            var bubble = ObjectPooler.DequeueObject(GameResources.GameSettings.BubblePoolKey);
            bubble.transform.position = transform.position + Vector3.up;
            bubble.SetActive(true);
            bubbles.Add(bubble);
        }

        private void Update()
        {
            if (bubbleSpawnRate.IsReady)
            {
                SpawnBubble();
                bubbleSpawnRate.StartCooldown(UnityEngine.Random.Range(3f, 6f));
            }
            for (int i = bubbles.Count - 1; i >= 0; i--)
            {
                if (bubbles[i].transform.position.y > maxY)
                {
                    ObjectPooler.EnqueueObject(bubbles[i], GameResources.GameSettings.BubblePoolKey);
                    bubbles.RemoveAt(i);
                }
                else
                {
                    bubbles[i].transform.position += (Vector3.up * 5f + Vector3.right * Mathf.Sin(Time.time)) * Time.deltaTime;
                }
            }
        }
    }
}