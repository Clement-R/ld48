using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Game

{
    [CreateAssetMenu(fileName = "Wave", menuName = "Arena/Wave", order = 0)]
    public class Wave : ScriptableObject
    {
        [SerializeField] private List<Enemy> m_toSpawn;

        private List<GameObject> m_spawned = new List<GameObject>();

        public List<GameObject> Spawn(Spawner p_spawner)
        {
            foreach (var enemy in m_toSpawn)
            {
                m_spawned.Add(p_spawner.Spawn(enemy.gameObject));
            }

            return m_spawned;
        }

        public bool IsDone()
        {
            //TODO: rework if better health system
            return m_spawned.All(e => e == null);
        }
    }
}