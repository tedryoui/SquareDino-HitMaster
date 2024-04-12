using UnityEngine;

namespace DefaultNamespace.Weapon
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public Bullet projectile;

        public int projectileSpeed;
        public int damage;

        public virtual Bullet Construct()
        {
            var bullet = Instantiate(projectile, null);
            
            bullet.SetDamage(damage);
            bullet.SetSpeed(projectileSpeed);

            return bullet;
        }
    }
}