using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameOverMenu : Menu
    {
        [SerializeField] private Button m_restartButton;
        [SerializeField] private Button m_mainMenuButton;

        private GameManager m_gameManager;

        private void Start()
        {
            m_gameManager = GameManager.Instance;

            m_restartButton.onClick.AddListener(Restart);
            m_mainMenuButton.onClick.AddListener(GoToMainMenu);

            m_gameManager.GameState.OnValueChanged += GameStateChanged;
            GameStateChanged(m_gameManager.GameState.Value);

            m_group.Hide();
        }

        private void OnDestroy()
        {
            m_gameManager.GameState.OnValueChanged -= GameStateChanged;
        }

        private void GameStateChanged(EGameState p_state)
        {
            if (p_state == EGameState.GAME_OVER)
            {
                m_group.Show();
            }
            else
            {
                m_group.Hide();
            }
        }

        private void Restart()
        {
            m_gameManager.SetGameState(EGameState.GAME);
            m_gameManager.Restart();
        }

        private void GoToMainMenu()
        {
            m_gameManager.SetGameState(EGameState.MAIN_MENU);
        }
    }
}