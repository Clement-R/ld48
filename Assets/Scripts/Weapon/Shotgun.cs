using UnityEngine;

using Cake.Data;

namespace Game
{
    public class Shotgun : Weapon
    {
        [SerializeField] private int m_bulletPerShot = 6;
        [SerializeField] private ValueRange m_spread;
        [SerializeField] protected ValueRange m_bulletSpeed;

        protected override void Shoot()
        {
            for (int i = 0; i < m_bulletPerShot; i++)
            {
                var bullet = SpawnBullet();
                bullet.Setup(m_bulletPower, Quaternion.Euler(0f, 0f, m_spread.Random()) * (transform.right * m_bulletSpeed.Random()));
            }
        }
    }
}