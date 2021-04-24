using UnityEngine;

using Game.Shared;

namespace Game
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private LayersConfig m_layersConfig;

        private void Start()
        {
            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();
        }

        private void OnCollisionEnter2D(Collision2D p_other)
        {
            var layer = p_other.gameObject.layer;
            if (m_layersConfig.Enemy.value == (m_layersConfig.Enemy.value | (1 << layer)))
            {
                //TODO:
                Debug.Log("Collision with an enemy");
            }
        }
    }
}