using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletWithParticles : Bullet
    {
        private ParticleSystem _explodeParticleSystem;

        private int _explodeForce;

        public void SetExplodeForce(int force)
        {
            _explodeForce = force;
        }
        
        public void SetParticleSystem(ParticleSystem particleSystem)
        {
            _explodeParticleSystem = particleSystem;
        }
        
        protected override void OnDispose(Bullet bullet)
        {
            InstantiateExplodeParticles();

            base.OnDispose(bullet);
        }

        private void InstantiateExplodeParticles()
        {
            var system = Instantiate(_explodeParticleSystem, null);

            system.transform.position = transform.position;
            system.transform.rotation = transform.rotation;
            system.transform.localScale = transform.localScale;
            
            system.Play();
        }

        protected override void CollisionPostProcess(Enemy enemy)
        {
            enemy.Punch(transform.position, transform.forward, _explodeForce);
        }
    }
}