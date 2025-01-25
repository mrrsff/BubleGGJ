using System;
using UnityEngine;

namespace Karma.Physics
{
    [RequireComponent(typeof(Collider2D))]
    public class Collision2DReporter : MonoBehaviour
    {
        public event Action<Collision2D> OnCollisionEnterEvent;
        public event Action<Collision2D> OnCollisionExitEvent;
        public event Action<Collision2D> OnCollisionStayEvent;
        public event Action<Collider2D> OnTriggerEnterEvent;
        public event Action<Collider2D> OnTriggerExitEvent;
        public event Action<Collider2D> OnTriggerStayEvent;
       
        private void OnCollisionEnter2D(Collision2D other) => OnCollisionEnterEvent?.Invoke(other);
        private void OnCollisionExit2D(Collision2D other) => OnCollisionExitEvent?.Invoke(other);
        private void OnCollisionStay2D(Collision2D other) => OnCollisionStayEvent?.Invoke(other);
        private void OnTriggerEnter2D(Collider2D other) => OnTriggerEnterEvent?.Invoke(other);
        private void OnTriggerExit2D(Collider2D other) => OnTriggerExitEvent?.Invoke(other);
        private void OnTriggerStay2D(Collider2D other) => OnTriggerStayEvent?.Invoke(other);
    }
}