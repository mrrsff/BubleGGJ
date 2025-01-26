using Karma.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ2025
{
    public class BubbleMovement : MonoBehaviour
    {
        public bool OnlyUpwards = true;
        private float randomOffset;
        private float randomSpeed;
        private float randomSize;
        private Vector2 randomAmplitude;

        private Vector3 startPosition;
        private void Awake()
        {
            startPosition = transform.position;
            randomOffset = Random.Range(0, 2 * Mathf.PI);
            randomSpeed = Random.Range(0.5f, 1.5f);
            randomSize = Random.Range(0.5f, 1.5f);
            randomAmplitude.x = Random.Range(0.5f, 1.5f);
            randomAmplitude.y = 1.5f - randomAmplitude.x;
            if (Random.value > 0.5) randomAmplitude.x *= -1;
            transform.localScale *= randomSize;
        }
        
        private void Update()
        {
            if (OnlyUpwards)
            {
                transform.position += Vector3.up * Time.deltaTime * randomAmplitude.y;
            }
            else
            {
                var x = Mathf.Cos(Time.time * randomSpeed + randomOffset) * randomAmplitude.x;
                var y = Mathf.Sin(Time.time * randomSpeed + randomOffset) * randomAmplitude.y;
                transform.position = startPosition.Add(x: x, y: y);   
            }
        }
    }
}