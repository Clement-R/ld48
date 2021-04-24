using UnityEngine;

namespace Game
{
    public class Pistol : WeaponBehavior
    {
        [SerializeField] protected float m_bulletSpeed;

        protected override void Shoot()
        {
            var bullet = SpawnBullet();
            bullet.Setup(m_bulletPower, transform.right * m_bulletSpeed);
        }
    }
}