using UnityEngine;

namespace DefaultNamespace.Weapon
{
    [CreateAssetMenu(fileName = "Weapon With Particles Data", menuName = "Weapon With Particles Data", order = 0)]
    public class WeaponWIthParticlesData : WeaponData
    {
        public int explodeForce;
        public ParticleSystem hitProjectileParticles;

        public override Bullet Construct()
        {
            var bullet =  base.Construct();

            if (bullet is BulletWithParticles bulletWithParticles)
            {
                bulletWithParticles.SetParticleSystem(hitProjectileParticles);
                bulletWithParticles.SetExplodeForce(explodeForce);
            }
            
            return bullet;
        }
    }
}