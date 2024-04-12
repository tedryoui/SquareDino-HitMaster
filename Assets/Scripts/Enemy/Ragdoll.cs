using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private bool initialState;
        [SerializeField] private Rigidbody[] rigidbodies;

        private void Awake()
        {
            enabled = initialState;
        }

        [ContextMenu("Find Rigidbodies")]
        public void Find()
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        private void OnEnable() => EnableRigidbodies();

        private void EnableRigidbodies()
        {
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
        }

        private void OnDisable() => DisableRigidbodies();

        private void DisableRigidbodies()
        {
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }

        public void AddForce(Vector3 point, Vector3 direction, float force)
        {
            var closest = rigidbodies.OrderBy(x => Vector3.Distance(x.transform.position, point)).First();
            
            closest.AddForceAtPosition(force * direction, point, ForceMode.Impulse);
        }
    }
}