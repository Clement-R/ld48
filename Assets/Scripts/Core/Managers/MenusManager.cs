using UnityEngine;
using UnityEngine.SceneManagement;

using Cake.Utils;
using Cake.Utils.Data;

namespace Game
{
    public class MenusManager : Singleton<MenusManager>
    {
        private GameManager m_gameManager;

        void Start()
        {
            m_gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                m_gameManager.SetGameState(EGameState.PAUSE);
                m_gameManager.Pause.Value = !m_gameManager.Pause.Value;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                m_gameManager.SetGameState(EGameState.GAME_OVER);
            }
        }
    }
}