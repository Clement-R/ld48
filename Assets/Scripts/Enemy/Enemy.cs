using UnityEngine;

namespace Game
{
    public abstract class Enemy : PausableMonoBehaviour
    {
        [SerializeField] protected int m_life;
        [SerializeField] protected int m_speed;

        protected Rigidbody2D m_rb;

        protected override void Start()
        {
            base.Start();
            m_rb = GetComponent<Rigidbody2D>();
        }

        protected override void PauseChanged(bool p_pause)
        {
            m_rb.simulated = !p_pause;
        }
    }
}