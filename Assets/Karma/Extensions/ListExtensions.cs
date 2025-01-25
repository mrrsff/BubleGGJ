using System.Collections.Generic;
using UnityEngine;

namespace Karma.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
        
        public static IList<T> GetRandomElements<T>(this IList<T> list, int count)
        {
            var randomElements = new List<T>();
            var shuffledList = new List<T>(list);
            shuffledList.Shuffle();
            for (int i = 0; i < count; i++)
            {
                randomElements.Add(shuffledList[i]);
            }

            return randomElements;
        }
        
        public static T GetRandomElement<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
        
        public static T GetRandomElement<T>(this IList<T> list, T exclude)
        {
            var randomElement = list.GetRandomElement();
            while (randomElement.Equals(exclude))
            {
                randomElement = list.GetRandomElement();
            }

            return randomElement;
        }
        
        public static T GetRandomElement<T>(this IList<T> list, IList<T> exclude)
        {
            var randomElement = list.GetRandomElement();
            while (exclude.Contains(randomElement))
            {
                randomElement = list.GetRandomElement();
            }

            return randomElement;
        }
        
        public static T GetRandomElement<T>(this IList<T> list, System.Func<T, bool> predicate)
        {
            var randomElement = list.GetRandomElement();
            while (!predicate(randomElement))
            {
                randomElement = list.GetRandomElement();
            }

            return randomElement;
        }
        
        public static T GetRandomElement<T>(this IList<T> list, System.Func<T, bool> predicate, T exclude)
        {
            var randomElement = list.GetRandomElement(exclude);
            while (!predicate(randomElement))
            {
                randomElement = list.GetRandomElement(exclude);
            }

            return randomElement;
        }
        
        public static T GetRandomElement<T>(this IList<T> list, System.Func<T, bool> predicate, IList<T> exclude)
        {
            var randomElement = list.GetRandomElement(exclude);
            while (!predicate(randomElement))
            {
                randomElement = list.GetRandomElement(exclude);
            }

            return randomElement;
        }
        
        public static T GetRandomElement<T>(this IList<T> list, System.Func<T, bool> predicate, System.Func<T, bool> exclude)
        {
            var randomElement = list.GetRandomElement();
            while (!predicate(randomElement) || exclude(randomElement))
            {
                randomElement = list.GetRandomElement();
            }

            return randomElement;
        }
    }
}