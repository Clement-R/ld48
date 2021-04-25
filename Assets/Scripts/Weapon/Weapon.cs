using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Cake.Pooling;

using DG.Tweening;

using Game.Shared;

namespace Game
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform m_muzzle;
        [SerializeField] protected float m_cooldown;
        [SerializeField] protected int m_bulletPower;
        [SerializeField] protected Bullet m_bulletPrefab;

        protected float m_nextShot = 0f;

        private bool m_canShoot => Time.time > m_nextShot;

        private LayersConfig m_layersConfig;
        private Rigidbody2D m_rb;
        private PolygonCollider2D m_collider;

        private bool m_thrown = false;

        private async void Awake()
        {
            await ConfigsManager.WaitForInstance();

            m_rb = GetComponent<Rigidbody2D>();
            m_rb.constraints = RigidbodyConstraints2D.FreezeAll;

            m_collider = GetComponent<PolygonCollider2D>();
            m_collider.isTrigger = true;

            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();
            gameObject.layer = (int) Mathf.Log(m_layersConfig.WeaponPickup.value, 2);
        }

        internal void Equipped()
        {
            m_rb.simulated = false;
            gameObject.layer = (int) Mathf.Log(m_layersConfig.Weapon.value, 2);
            m_collider.isTrigger = false;
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

        private void OnTriggerStay2D(Collider2D p_other)
        {
            if (m_layersConfig.Player.value == (m_layersConfig.Player.value | (1 << p_other.gameObject.layer)))
            {
                if (p_other.gameObject.TryGetComponent(out PlayerWeapon playerWeapon))
                {
                    playerWeapon.EquipWeapon(this);
                }
            }
        }

        public void Throw(Vector2 p_direction)
        {
            transform.SetParent(GameManager.Instance.CurrentArena.transform);

            m_rb.constraints = RigidbodyConstraints2D.None;
            m_rb.simulated = true;
            m_rb.AddForce(p_direction, ForceMode2D.Impulse);
            m_thrown = true;
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            if (!m_thrown)
                return;

            if (m_layersConfig.Ground.value == (m_layersConfig.Ground.value | (1 << p_other.gameObject.layer)))
            {
                m_rb.drag = 50f;
                m_rb.gravityScale *= 10f;
                gameObject.layer = (int) Mathf.Log(m_layersConfig.Persistant.value, 2);
            }
        }

        protected abstract void Shoot(); //→ change bullet pattern here

        protected Bullet SpawnBullet()
        {
            return SimplePool.Spawn(m_bulletPrefab, m_muzzle.position, Quaternion.identity);
        }
    }
}