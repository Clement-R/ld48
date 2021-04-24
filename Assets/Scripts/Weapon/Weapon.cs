using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Cake.Pooling;

namespace Game
{
    public abstract class WeaponBehavior : MonoBehaviour
    {
        [SerializeField] protected Transform m_muzzle;
        [SerializeField] protected float m_cooldown;
        [SerializeField] protected int m_bulletPower;
        [SerializeField] protected Bullet m_bulletPrefab;

        protected float m_nextShot = 0f;

        private bool m_canShoot => Time.time > m_nextShot;

        private Rigidbody2D m_rb;

        private void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_rb.simulated = false;
        }

        public bool TryToShoot()
        {
            if (m_canShoot)
            {
                Shoot();
                m_nextShot = Time.time + m_cooldown;
                return true;
            }

            return false;
        }

        public void Throw(Vector2 p_direction)
        {
            m_rb.simulated = true;
            m_rb.AddForce(p_direction, ForceMode2D.Impulse);
        }

        protected abstract void Shoot(); //→ change bullet pattern here

        protected Bullet SpawnBullet()
        {
            return SimplePool.Spawn(m_bulletPrefab, m_muzzle.position, Quaternion.identity);
        }
    }
}