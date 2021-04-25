using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Cake.Genoise;
using Cake.Opera.Data;
using Cake.Utils;
using Cake.Utils.Data;

namespace Game
{
    public class GameManager : Singleton<GameManager>
    {
        public Action<string> OnArenaChange; // arena name 
        public Action OnGameStart;

        public GameObject CurrentArena => m_currentArena.gameObject;

        public ListenableValue<bool> Pause;
        public ListenableValue<EGameState> GameState;
        [SerializeField] private List<Arena> m_levels;

        [SerializeField] private TransitionUI m_transition;
        [SerializeField] private RawImage m_background;
        [SerializeField] private InBetween m_inBetween;
        [SerializeField] private SFXSound m_winSound;

        private int m_levelIndex = -1;
        private ArenaManager m_currentArena = null;
        private string m_arenaName = string.Empty;

        public void SetGameState(EGameState p_state)
        {
            if ((GameState.Value == EGameState.GAME || GameState.Value == EGameState.PAUSE || GameState.Value == EGameState.GAME_OVER || GameState.Value == EGameState.WIN) && p_state == EGameState.MAIN_MENU)
            {
                Routine.Start(_GameToMainMenu());
            }
            else if (GameState.Value == EGameState.MAIN_MENU && p_state == EGameState.GAME)
            {
                Routine.Start(_MainMenuToGame());
            }
            else if (GameState.Value == EGameState.GAME && p_state == EGameState.WIN)
            {
                SoundsManager.Instance.Play(m_winSound);
                GameState.Value = p_state;
            }
            else
            {
                GameState.Value = p_state;
            }
        }

        private IEnumerator _MainMenuToGame()
        {
            Pause.Value = true;
            yield return m_transition.Show();
            StartGame();
            yield return m_transition.Hide();
            Pause.Value = false;
        }

        private IEnumerator _GameToMainMenu()
        {
            Pause.Value = true;
            yield return m_transition.Show();
            StopGame();
            GameState.Value = EGameState.MAIN_MENU;
            yield return m_transition.Hide();
            Pause.Value = false;
        }

        private void StartGame()
        {
            m_levelIndex = -1;
            NextArena();
            GameState.Value = EGameState.GAME;
            OnGameStart?.Invoke();
        }

        private void NextArena()
        {
            m_levelIndex++;

            // Win condition
            if (m_levelIndex >= m_levels.Count)
            {
                SetGameState(EGameState.WIN);
                return;
            }

            // Transition to arena
            if (m_currentArena != null)
            {
                Routine.Start(_InBetweenArenas());
            }
            else
            {
                SpawnArena(m_levels[m_levelIndex]);
                StartArena();
            }
        }

        private IEnumerator _InBetweenArenas()
        {
            Pause.Value = true;

            // Transition to in between and remve old arena
            yield return m_transition.Show();
            m_inBetween.StartEffect(m_currentArena.Color, 2f);
            m_inBetween.gameObject.SetActive(true);
            Destroy(m_currentArena.gameObject);
            yield return m_transition.Hide();

            yield return new WaitForSeconds(2f);

            // Hide in between and spawn arena
            yield return m_transition.Show();
            m_inBetween.gameObject.SetActive(false);
            SpawnArena(m_levels[m_levelIndex]);
            yield return m_transition.Hide();

            Pause.Value = false;

            StartArena();
        }

        private void SpawnArena(Arena p_arena)
        {
            var arena = p_arena.ArenaPrefab.Instantiate();

            m_currentArena = arena.GetComponent<ArenaManager>();

            Color.RGBToHSV(p_arena.Color, out float h, out float s, out float v);
            var desaturatedColor = Color.HSVToRGB(h, s - 0.2f, v, false);
            m_currentArena.SetColor(desaturatedColor);
            m_currentArena.OnArenaFinished += NextArena;

            m_background.material.SetColor("_Color", p_arena.Color);

            m_arenaName = p_arena.Name;
        }

        private void StartArena()
        {
            m_currentArena.StartArena();
            OnArenaChange?.Invoke(m_arenaName);
        }

        private void StopGame()
        {
            Destroy(m_currentArena.gameObject);
            m_currentArena = null;
            m_levelIndex = -1;
        }

        public void Restart()
        {
            StopGame();
            StartGame();
        }
    }
}