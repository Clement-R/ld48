using UnityEngine;

using Game.Shared;

namespace Game
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private LayersConfig m_layersConfig;
        private Health m_health;
        private PlayerControler m_playerController;

        private void Start()
        {
            m_health = GetComponent<Health>();
            m_playerController = GetComponent<PlayerControler>();

            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            var layer = p_other.gameObject.layer;
            if (m_layersConfig.Enemy.value == (m_layersConfig.Enemy.value | (1 << layer)))
            {
                m_health.TakeDamage(1);
                Vector2 direction = p_other.gameObject.transform.position.x > transform.position.x ? -Vector2.right : Vector2.right;
                m_playerController.KnockBack(direction);
            }
        }
    }
}