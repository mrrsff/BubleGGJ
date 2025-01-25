using UnityEngine;

namespace GGJ2025
{
    public abstract class PowerUp<T> : BasePowerUp where T : BaseBrawlerComponent 
    {
    }
}