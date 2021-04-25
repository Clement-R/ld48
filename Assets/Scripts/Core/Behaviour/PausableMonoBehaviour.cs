using UnityEngine;

using Cake.Millefeuille;

namespace Game
{
    public abstract class PausableMonoBehaviour : MonoBehaviour
    {
        protected GameManager m_gameManager = null;

        protected virtual void Start()
        {
            m_gameManager = GameManager.Instance;
            m_gameManager.Pause.OnValueChanged += PauseChanged;
        }

        protected virtual void OnDestroy()
        {
            if (m_gameManager != null)
                m_gameManager.Pause.OnValueChanged -= PauseChanged;
        }

        protected abstract void PauseChanged(bool p_pause);

        protected virtual void Update()
        {
            if (m_gameManager.Pause.Value)
                return;
        }

        protected virtual void FixedUpdate()
        {
            if (m_gameManager.Pause.Value)
                return;
        }
    }
}