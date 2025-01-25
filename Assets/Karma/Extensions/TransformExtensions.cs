using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Karma.Extensions
{
    public static class TransformExtensions
    {
        public static IEnumerable<Transform> Children(this Transform transform)
        {
            foreach (Transform child in transform)
                yield return child;
        }
        
        public static IEnumerable<Transform> Descendants(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                yield return child;
                foreach (var grandChild in child.Descendants())
                    yield return grandChild;
            }
        }
        
        public static IEnumerable<Transform> DescendantsAndSelf(this Transform transform)
        {
            yield return transform;
            foreach (var descendant in transform.Descendants())
                yield return descendant;
        }
        
        public static IEnumerable<Transform> Parents(this Transform transform)
        {
            var parent = transform.parent;
            while (parent != null)
            {
                yield return parent;
                parent = parent.parent;
            }
        }
        
        public static IEnumerable<Transform> ParentsAndSelf(this Transform transform)
        {
            yield return transform;
            foreach (var parent in transform.Parents())
                yield return parent;
        }

        public static string GetPath(this Transform transform)
        {
            StringBuilder path = new StringBuilder(transform.name);
            while (transform.parent != null)
            {
                transform = transform.parent;
                path.Insert(0, transform.name + "/");
            }
            return path.ToString();
        }
    }
}