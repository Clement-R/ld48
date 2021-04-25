using UnityEngine;

using Cake.Genoise;
using Cake.Pooling;

using Game.Shared;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float m_timeToLive = 5f;

        private Rigidbody2D m_rb;
        private LayersConfig m_layersConfig;
        private int m_power;
        private float m_despawnTime = float.MinValue;

        private void Start()
        {
            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();
        }

        public void Setup(int p_power, Vector3 p_velocity)
        {
            m_power = p_power;

            m_rb = GetComponent<Rigidbody2D>();
            m_rb.velocity = p_velocity;

            var angle = Mathf.Atan2(m_rb.velocity.y, m_rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector2.right);

            transform.right = Vector2.right * m_rb.velocity.x;
            m_despawnTime = Time.time + m_timeToLive;
        }

        private void Update()
        {
            if (m_despawnTime != float.MinValue && Time.time > m_despawnTime)
            {
                Despawn();
            }
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            var layer = p_other.gameObject.layer;
            if (m_layersConfig.Enemy.value == (m_layersConfig.Enemy.value | (1 << layer)))
            {
                if (p_other.gameObject.TryGetComponent(out Health health))
                {
                    if (health.Alive)
                    {
                        health.TakeDamage(m_power);
                    }
                }

                //TODO: enemy hit effect

                // Vector2 direction = p_other.gameObject.transform.position.x > transform.position.x ? -Vector2.right : Vector2.right;
                // m_playerController.KnockBack(direction);

                Destroy(gameObject);
                return;
            }

            if (m_layersConfig.Ground.value == (m_layersConfig.Ground.value | (1 << layer)))
            {
                Destroy(gameObject);
            }
        }

        private void Despawn()
        {
            SimplePool.Despawn(gameObject);
        }
    }
}