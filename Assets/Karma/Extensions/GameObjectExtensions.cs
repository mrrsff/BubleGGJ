using UnityEngine;

namespace Karma.Extensions
{
    public static class GameObjectExtensions
    {
        public static T OrNull<T>(this Object obj) where T : Object => obj ? obj as T : null;
        
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
                component = gameObject.AddComponent<T>();
            return component;
        }
        public static void SetLayer(this GameObject gameObject, int layer, bool recursive = false)
        {
            gameObject.layer = layer;
            if (!recursive) return;
            
            foreach (Transform child in gameObject.transform)
                child.gameObject.SetLayer(layer, true);
        }
        public static void SetLayer(this GameObject gameObject, string layerName, bool recursive = false)
        {
            gameObject.SetLayer(LayerMask.NameToLayer(layerName), recursive);
        }
        public static void SetTag(this GameObject gameObject, string tag, bool recursive = false)
        {
            gameObject.tag = tag;
            if (!recursive) return;
            
            foreach (Transform child in gameObject.transform)
                child.gameObject.SetTag(tag, true);
        }

        public static T[] GetComponentsInDirectChildren<T>(this GameObject gameObject)
        {
            var components = new T[gameObject.transform.childCount];
            for (int i = 0; i < gameObject.transform.childCount; i++)
                components[i] = gameObject.transform.GetChild(i).GetComponent<T>();
            return components;
        }
    }
}