using UnityEngine;

namespace GGJ2025
{
    public class ForceReceiverComponent : BaseBrawlerComponent
    {
        public override void OnHit(HitInfo hitInfo)
        {
            if (hitInfo.Force > 0)
            {
                Brawler.Rigidbody.AddForce(hitInfo.Direction * hitInfo.Force);
            }
            else Debug.Log("Force is 0, no force applied");
        }
    }
}