using UnityEngine;

namespace Karma.Physics
{
    [RequireComponent(typeof(Collider))]
    public class CollisionReporter : MonoBehaviour
    {
        public event System.Action<Collision> OnCollisionEnterEvent;
        public event System.Action<Collision> OnCollisionExitEvent;
        public event System.Action<Collider> OnCollisionStayEvent;
        public event System.Action<Collider> OnTriggerEnterEvent;
        public event System.Action<Collider> OnTriggerExitEvent;
        public event System.Action<Collider> OnTriggerStayEvent;
        private void OnCollisionEnter(Collision collision) => OnCollisionEnterEvent?.Invoke(collision);
        private void OnCollisionExit(Collision collision) => OnCollisionExitEvent?.Invoke(collision);
        private void OnCollisionStay(Collision collision) => OnCollisionStayEvent?.Invoke(collision.collider);
        private void OnTriggerEnter(Collider other) => OnTriggerEnterEvent?.Invoke(other);
        private void OnTriggerExit(Collider other) => OnTriggerExitEvent?.Invoke(other);
        private void OnTriggerStay(Collider other) => OnTriggerStayEvent?.Invoke(other);
    }
}
