using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Tilemaps;

using Cake.Data;

namespace Game
{
    public class ArenaManager : PausableMonoBehaviour
    {
        public Action OnArenaFinished;

        [SerializeField] private Spawner m_spawner;
        [SerializeField] private WeaponSpawner m_weaponSpawner;
        [SerializeField] private List<Wave> m_waves;
        [SerializeField] private ValueRange m_delayBetweenWaves;
        [SerializeField] private ValueRange m_delayBetweenWeaponSpawn;
        [SerializeField] private TilemapRenderer m_tilemapRenderer;

        private List<Wave> m_currentWaves = new List<Wave>();
        private int m_lastWaveIndex = -1;
        private float m_nextSpawn;
        private float m_nextWeaponSpawn;
        private bool m_isStarted = false;
        private bool m_isDone = false;
        private bool m_currentIsLastWave => m_lastWaveIndex + 1 >= m_waves.Count;

        public void SetColor(Color p_color)
        {
            var tileMaterial = m_tilemapRenderer.material;
            tileMaterial.SetColor("_Color", p_color);
        }

        [ContextMenu("Start arena")]
        public void StartArena()
        {
            m_isStarted = true;
            m_weaponSpawner.Active = true;
            m_nextWeaponSpawn = m_delayBetweenWeaponSpawn.Random();
            m_lastWaveIndex = -1;
            NextWave();
        }

        private void NextWave()
        {
            if (m_currentIsLastWave)
            {
                return;
            }

            m_lastWaveIndex++;
            var wave = Instantiate(m_waves[m_lastWaveIndex]);
            m_currentWaves.Add(wave);
            wave.Spawn(m_spawner);

            m_nextSpawn = m_delayBetweenWaves.Random();

            m_weaponSpawner.Spawn();
        }

        protected override void Update()
        {
            base.Update();

            if (!m_isStarted || m_isDone)
                return;

            m_nextSpawn -= Time.deltaTime;
            m_nextWeaponSpawn -= Time.deltaTime;

            // Check next wave
            if (m_currentWaves.All(e => e.IsDone()) || (!m_currentIsLastWave && m_nextSpawn <= 0f))
            {
                NextWave();
            }

            // Spawn weapon
            if (m_nextWeaponSpawn <= 0f)
            {
                m_weaponSpawner.Spawn();
                m_nextWeaponSpawn = m_delayBetweenWeaponSpawn.Random();
            }

            // Clear and check finish condition
            m_currentWaves.RemoveAll(e => e.IsDone());
            if (m_currentWaves.Count == 0 && m_currentIsLastWave)
            {
                OnArenaFinished?.Invoke();
                m_isDone = true;
            }
        }

        protected override void PauseChanged(bool p_pause)
        {

        }
    }
}