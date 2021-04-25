using System.Threading.Tasks;

using UnityEngine;

using DG.Tweening;

namespace Game
{
    public class SquareTransition : Transition
    {
        [SerializeField] private Transform m_square;
        [SerializeField] private Transform m_showPosStart;
        [SerializeField] private Transform m_showPosEnd;
        [SerializeField] private Transform m_hidePosStart;
        [SerializeField] private Transform m_hidePosEnd;
        [SerializeField] private float m_duration = 1f;

        public override Task Show()
        {
            m_square?.DOKill();
            return m_square.DOMoveY(m_showPosEnd.position.y, m_duration, true).From(m_showPosStart.position.y).SetEase(Ease.Linear).SetDelay(0.25f).AsyncWaitForCompletion();
        }

        public override Task Hide()
        {
            m_square?.DOKill();
            return m_square.DOMoveY(m_hidePosEnd.position.y, m_duration, true).From(m_hidePosStart.position.y).SetEase(Ease.Linear).SetDelay(0.25f).AsyncWaitForCompletion();
        }
    }
}