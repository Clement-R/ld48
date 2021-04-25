using UnityEngine;

namespace Game
{
    public abstract class Enemy : PausableMonoBehaviour
    {
        [SerializeField] protected int m_speed;

        protected Rigidbody2D m_rb;
        protected Health m_health;

        protected override void Start()
        {
            base.Start();
            m_rb = GetComponent<Rigidbody2D>();
            m_health = GetComponent<Health>();

            m_health.OnDeath += Death;
        }

        private void Death()
        {
            if (TryGetComponent<DeathEffect>(out DeathEffect deathEffect))
            {
                deathEffect.Death();
            }
            else
            {
                Destroy(gameObject, 0.2f);
            }
        }

        protected override void PauseChanged(bool p_pause)
        {
            m_rb.simulated = !p_pause;
        }
    }
}