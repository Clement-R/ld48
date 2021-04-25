using UnityEngine;

namespace Game
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        private const string m_fall = "Fall";
        private const string m_walk = "Walk";
        private const string m_jump = "Jump";

        private Rigidbody2D m_rb;
        private PlayerControler m_playerController;

        private void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_playerController = GetComponent<PlayerControler>();

            m_playerController.OnJump += Jump;
        }

        private void Jump()
        {
            m_animator.SetTrigger(m_jump);
        }

        private void Update()
        {
            if (m_rb.velocity.y < 0f && !m_playerController.Grounded)
            {
                m_animator.SetBool(m_fall, true);
            }
            else
            {
                m_animator.SetBool(m_fall, false);
            }

            if (m_playerController.CurrentInput.x != 0f && m_playerController.Grounded)
            {
                m_animator.SetBool(m_walk, true);
            }
            else
            {
                m_animator.SetBool(m_walk, false);
            }
        }
    }
}