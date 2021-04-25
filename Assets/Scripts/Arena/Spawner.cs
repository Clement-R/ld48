using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Shared;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_spawners;

        private LayersConfig m_layersConfig;

        private void Awake()
        {
            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();
        }

        public GameObject Spawn(GameObject p_template)
        {
            var spawner = m_spawners.Random();
            while (Physics2D.OverlapBox(spawner.transform.position, new Vector2(10f, 10f), 0f, m_layersConfig.Player.value))
            {
                spawner = m_spawners.Random();
            }

            return Instantiate(p_template, spawner.transform.position, spawner.transform.rotation, transform);
        }

        private void OnDrawGizmos()
        {
            foreach (var spawner in m_spawners)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(spawner.position, 2.5f);
            }
        }
    }
}