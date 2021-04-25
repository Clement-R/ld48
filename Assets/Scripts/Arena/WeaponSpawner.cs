using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game
{
    public class WeaponSpawner : MonoBehaviour
    {
        public bool Active = false;

        [SerializeField] private List<Transform> m_spawners = new List<Transform>();
        [SerializeField] private List<Weapon> m_weapons = new List<Weapon>();

        private Weapon m_lastSpawned = null;

        public void Spawn()
        {
            Weapon weapon = m_weapons.Random();
            if (m_lastSpawned != null)
            {
                while (weapon == m_lastSpawned)
                {
                    weapon = m_weapons.Random();
                }
            }

            Spawn(weapon.gameObject);
            m_lastSpawned = weapon;
        }

        private GameObject Spawn(GameObject p_template)
        {
            var spawner = m_spawners.Random();
            return Instantiate(p_template, spawner.transform.position, spawner.transform.rotation, transform);
        }

        private void OnDrawGizmos()
        {
            foreach (var spawner in m_spawners)
            {
                if (spawner == null)
                    continue;

                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(spawner.position, 2f);
            }
        }
    }
}