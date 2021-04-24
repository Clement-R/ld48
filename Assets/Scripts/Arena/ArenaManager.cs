using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Cake.Data;

namespace Game
{
    public class ArenaManager : MonoBehaviour
    {
        public Action OnArenaFinished;

        [SerializeField] private Spawner m_spawner;
        [SerializeField] private List<Wave> m_waves;
        [SerializeField] private ValueRange m_delayBetweenWaves;

        private List<Wave> m_currentWaves = new List<Wave>();
        private int m_lastWaveIndex = -1;
        private float m_nextSpawn;
        private bool m_isStarted = false;
        private bool m_isDone = false;

        [ContextMenu("Start arena")]
        public void StartArena()
        {
            m_isStarted = true;
            m_lastWaveIndex = -1;
            NextWave();
        }

        private void NextWave()
        {
            var nextIndex = m_lastWaveIndex + 1;
            if (nextIndex >= m_waves.Count)
            {
                OnArenaFinished?.Invoke();
                m_isDone = true;
                return;
            }

            var wave = Instantiate(m_waves[nextIndex]);
            m_currentWaves.Add(wave);
            wave.Spawn(m_spawner);

            m_lastWaveIndex = nextIndex;

            m_nextSpawn = Time.time + m_delayBetweenWaves.Random();
        }

        private void Update()
        {
            if (!m_isStarted || m_isDone)
                return;

            // Check next wave
            if (m_currentWaves.All(e => e.IsDone()) || (m_lastWaveIndex < m_waves.Count && Time.time > m_nextSpawn))
            {
                NextWave();
            }

            // Clear
            m_currentWaves.RemoveAll(e => e.IsDone());
        }
    }
}