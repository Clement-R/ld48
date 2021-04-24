using System;

using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public Action OnDeath;

        public bool Alive => m_health > 0;
        public int Value => m_health;
        [SerializeField] private int m_baseHealth;

        private int m_health;

        private void Start()
        {
            m_health = m_baseHealth;
        }

        public void TakeDamage(int p_damages)
        {
            m_health = Mathf.Clamp(m_health - p_damages, 0, m_baseHealth);

            if (m_health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}