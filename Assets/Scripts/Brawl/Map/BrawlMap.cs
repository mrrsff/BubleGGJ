using System;
using System.Collections.Generic;
using System.Linq;
using Karma.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ2025
{
    public class BrawlMap : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointsParent;
        public IEnumerable<Transform> spawnPoints => spawnPointsParent.Children();
    }
}