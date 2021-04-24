using UnityEngine;

using Cake.Genoise;
using Cake.Pooling;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        public int Power
        {
            get;
            private set;
        }

        [SerializeField] private float m_timeToLive = 5f;

        private Rigidbody2D m_rb;
        private float m_despawnTime = float.MinValue;

        public void Setup(int p_power, Vector3 p_velocity)
        {
            Power = p_power;

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            //TODO:
        }

        private void Despawn()
        {
            SimplePool.Despawn(gameObject);
        }
    }
}