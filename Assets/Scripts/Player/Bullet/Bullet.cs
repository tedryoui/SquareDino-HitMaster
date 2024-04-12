using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TrailRenderer trail;

        [Header("Data")] 
        [SerializeField] private LayerMask physicsOverlapMask;
        [Space(5)]
        [SerializeField] private Vector3 physicSphereCenter;
        [SerializeField] private float physicSphereRadius = 0.25f;

        [HideInInspector] public UnityEvent<Bullet> dispose;
        
        private float _speed;
        private int _damage;

        private void Update()
        {
            Move();
            OverlapForCollisions();
        }

        public void SetDamage(int damage) => _damage = damage;

        public void SetSpeed(int speed) => _speed = speed;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            
            trail.Clear();
        }
        
        public void SetDirection(Vector3 direction) => transform.forward = direction;

        private void Move()
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }
        
        private void OverlapForCollisions()
        {
            var results = new Collider[1];
            var overlapSphereNonAlloc = 
                Physics.OverlapSphereNonAlloc(transform.position + transform.TransformVector(physicSphereCenter), physicSphereRadius, results, physicsOverlapMask);

            if (overlapSphereNonAlloc > 0)
            {
                if (results[0] != null)
                {
                    var enemy = results[0].GetComponent<Enemy>(); 
                    enemy.Damage(_damage);
                    
                    CollisionPostProcess(enemy);
                    
                    OnDispose(this);
                }
            }
        }

        protected virtual void CollisionPostProcess(Enemy enemy)
        {
            
        }

        protected virtual void OnDispose(Bullet bullet)
        {
            dispose?.Invoke(bullet);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position + transform.TransformVector(physicSphereCenter), physicSphereRadius);
        }
    }
}