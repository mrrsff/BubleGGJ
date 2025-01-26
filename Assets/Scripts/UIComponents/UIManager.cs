using Karma;
using UnityEngine;

namespace GGJ2025.UIComponents
{
    public class UIManager : Singleton<UIManager>
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        
    }
}