using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_spawners;

        public GameObject Spawn(GameObject p_template)
        {
            var spawner = m_spawners.Random();
            return Instantiate(p_template, spawner.transform.position, spawner.transform.rotation, transform);
        }

        private void OnDrawGizmos()
        {
            foreach (var spawner in m_spawners)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(spawner.position, 2f);
            }
        }
    }
}