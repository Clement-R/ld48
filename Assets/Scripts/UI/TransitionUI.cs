using System.Threading.Tasks;

using UnityEngine;

namespace Game
{
    public class TransitionUI : MonoBehaviour
    {
        [SerializeField] private Transition m_currentTransition;

        public Task Show()
        {
            return m_currentTransition.Show();
        }

        public Task Hide()
        {
            return m_currentTransition.Hide();
        }

        [ContextMenu("Show")]
        private void TestShow()
        {
            Show();
        }

        [ContextMenu("Hide")]
        private void TestHide()
        {
            Hide();
        }
    }
}