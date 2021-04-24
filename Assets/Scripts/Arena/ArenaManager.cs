using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Game
{
    public class ArenaManager : MonoBehaviour
    {
        [SerializeField] private Spawner m_spawner;
        [SerializeField] private Wave m_wave;

        private List<Wave> m_currentWaves = new List<Wave>();

        [ContextMenu("Test arena")]
        private void StartArena()
        {
            var wave = Instantiate(m_wave);
            wave.Spawn(m_spawner);
            m_currentWaves.Add(wave);
        }

        [ContextMenu("Wave done ?")]
        private void WaveDone()
        {
            Debug.Log(m_currentWaves.All(e => e.IsDone()));
        }
    }
}