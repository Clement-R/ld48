using UnityEngine;

using Cake.Data;

namespace Game
{
    public class MachineGun : WeaponBehavior
    {
        [SerializeField] protected float m_bulletSpeed;
        [SerializeField] private ValueRange m_spread;

        protected override void Shoot()
        {
            var bullet = SpawnBullet();
            bullet.Setup(m_bulletPower, Quaternion.Euler(0f, 0f, m_spread.Random()) * (transform.right * m_bulletSpeed));
        }
    }
}