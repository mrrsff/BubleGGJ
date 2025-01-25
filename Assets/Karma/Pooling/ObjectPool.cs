using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karma.Pooling
{
    public class ObjectPool: MonoBehaviour
    {
        /* This class is a generic object pool.
         * It can be used to pool any type of object that inherits from MonoBehaviour.
         * Unfortunately, it can't be used to pool objects those are game objects without a MonoBehaviour.
         */
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private int initialPoolSize = 10;
        private Queue<GameObject> pool = new Queue<GameObject>();
        int lastSpawnedIndex = 0;

        private void Start()
        {
            AddObjectsToPool(initialPoolSize);
        }

        private void AddObjectToPool()
        {
            var prefab = prefabs[lastSpawnedIndex];
            lastSpawnedIndex = (lastSpawnedIndex + 1) % prefabs.Length;

            var newObject = Object.Instantiate(prefab, transform, true);
            newObject.transform.position = Vector3.zero;
            newObject.transform.rotation = Quaternion.identity;
            newObject.transform.localScale = Vector3.one;
            newObject.gameObject.SetActive(false);
            pool.Enqueue(newObject);
        }

        private void AddObjectsToPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddObjectToPool();
            }
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
            {
                AddObjectToPool();
            }
            var objectToReturn = pool.Dequeue();
            objectToReturn.SetActive(true);
            return objectToReturn;
        }

        public void Return(GameObject objectToReturn)
        {
            objectToReturn.transform.position = Vector3.zero;
            objectToReturn.transform.rotation = Quaternion.identity;
            objectToReturn.transform.localScale = Vector3.one;
            objectToReturn.gameObject.SetActive(true);
            
            objectToReturn.SetActive(false);
            pool.Enqueue(objectToReturn);
        }

        public GameObject GetForSeconds(float seconds)
        {
            var obj = Get();
            StartCoroutine(ReturnAfterSeconds(obj, seconds));
            return obj;
        }
        
        private IEnumerator ReturnAfterSeconds(GameObject obj, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Return(obj);
        }
    }
}