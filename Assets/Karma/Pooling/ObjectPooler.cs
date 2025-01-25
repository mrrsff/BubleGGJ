using System.Collections.Generic;
using UnityEngine;

namespace Karma.Pooling
{
    public static class ObjectPooler
    {
        static int emptyPoolAdditionCount = 10;

        static Dictionary<string, Transform> parents = new();
        static Dictionary<string, Component> poolLookup = new();
        static Dictionary<string, Queue<Component>> poolDictionary = new();

        // Gameobject pooling
        static Dictionary<string, Queue<GameObject>> gameObjectPools = new();
        static Dictionary<string, GameObject> gameObjectLookup = new();
        public static void EnqueueObject(GameObject obj, string dictionaryKey)
        {
            if(!obj.activeSelf) return;

            obj.transform.position = Vector3.zero;
            obj.transform.SetParent(parents[dictionaryKey]);
            gameObjectPools[dictionaryKey].Enqueue(obj);
            obj.SetActive(false);
        }

        public static GameObject DequeueObject(string key)
        {
            // If the pool exists, try to dequeue an object
            if (gameObjectPools[key].TryDequeue(out GameObject obj))
            {
                return obj;
            }
            for (int i = 0; i < emptyPoolAdditionCount; i++)
            {
                EnqueueNewInstance(gameObjectLookup[key], key);
            }
            return DequeueObject(key);
        }

        public static GameObject EnqueueNewInstance(GameObject item, string dictionaryKey)
        {
            // Create a new object and add it to the pool
            GameObject obj = Object.Instantiate(item, parents[dictionaryKey]);
            obj.name = dictionaryKey;
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
            gameObjectPools[dictionaryKey].Enqueue(obj);
            return obj;
        }

        public static void SetupPool(GameObject pooledPrefab, int poolSize, string dictionaryKey)
        {
            if (gameObjectPools.ContainsKey(dictionaryKey))
                return;
            // Add the prefab to the lookup
            gameObjectLookup.Add(dictionaryKey, pooledPrefab);

            // Create a new queue of objects
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // Create a new parent object
            Transform parent = new GameObject(dictionaryKey + " Pool").transform;
            parents.Add(dictionaryKey, parent);
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Object.Instantiate(pooledPrefab, parent);
                obj.name = dictionaryKey;
                obj.SetActive(false);
                obj.transform.position = Vector3.zero;
                objectPool.Enqueue(obj);
            }
            gameObjectPools.Add(dictionaryKey, objectPool);
        }

        public static void EnqueueObject<T>(T obj, string dictionaryKey) where T : Component
        {
            if(!obj.gameObject.activeSelf) return;

            obj.transform.position = Vector3.zero;
            obj.transform.SetParent(parents[dictionaryKey]);
            poolDictionary[dictionaryKey].Enqueue(obj);
            obj.gameObject.SetActive(false);
        }

        public static T DequeueObject<T>(string key) where T : Component
        {
            // If the pool exists, try to dequeue an object
            if (poolDictionary[key].TryDequeue(out Component obj))
            {
                obj.gameObject.SetActive(true);
                return obj as T;
            }
            for (int i = 0; i < emptyPoolAdditionCount; i++)
            {
                EnqueueNewInstance(poolLookup[key] as T, key);
            }
            return DequeueObject<T>(key);
        }
        public static T EnqueueNewInstance<T>(T item, string dictionaryKey) where T : Component
        {
            // Create a new object and add it to the pool
            T obj = Object.Instantiate(item);
            obj.name = dictionaryKey;
            obj.gameObject.SetActive(false);
            obj.transform.position = Vector3.zero;
            poolDictionary[dictionaryKey].Enqueue(obj);
            return obj;
        }

        public static void SetupPool<T>(T pooledPrefab, int poolSize, string dictionaryKey) where T : Component
        {
            if (poolDictionary.ContainsKey(dictionaryKey))
                return;
            // Add the prefab to the lookup
            poolLookup.Add(dictionaryKey, pooledPrefab);

            // Create a new queue of objects
            Queue<Component> objectPool = new Queue<Component>();

            // Create a new parent object
            Transform parent = new GameObject(dictionaryKey + " Pool").transform;
            parents.Add(dictionaryKey, parent);
            for (int i = 0; i < poolSize; i++)
            {
                T obj = Object.Instantiate(pooledPrefab, parent);
                obj.name = dictionaryKey;
                obj.gameObject.SetActive(false);
                obj.transform.position = Vector3.zero;
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(dictionaryKey, objectPool);
        }
        
        public static void DestroyPool(string key)
        {
            foreach (var obj in poolDictionary[key])
            {
                Object.Destroy(obj.gameObject);
            }
            poolDictionary[key].Clear();
            poolDictionary.Remove(key);
            Object.Destroy(parents[key].gameObject);
            parents.Remove(key);
        }
    }
}
