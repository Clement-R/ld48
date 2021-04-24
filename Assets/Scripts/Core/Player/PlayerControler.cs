using System;
using System.Collections;

using UnityEngine;

using Game.Shared;

namespace Game
{
    public class PlayerControler : PausableMonoBehaviour
    {
        private PlayerManager m_playerManager = null;
        private PlayerConfig m_playerConfig = null;
        private LayersConfig m_layersConfig;

        private Rigidbody2D m_rb;
        private BoxCollider2D m_collider;
        private Vector2 m_input;
        private bool m_isGrounded = true;
        private bool m_initialized = false;

        protected async void Awake()
        {
            await ConfigsManager.WaitForInstance();

            m_rb = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<BoxCollider2D>();

            m_playerManager = PlayerManager.Instance;
            m_playerConfig = ConfigsManager.Instance.Get<PlayerConfig>();
            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();

            m_playerManager.Player = gameObject;
            m_initialized = true;
        }

        protected override void PauseChanged(bool p_pause)
        {
            m_rb.simulated = !p_pause;
        }

        protected override void Update()
        {
            base.Update();

            if (!m_initialized)
                return;

            m_isGrounded = CheckGrounded();

            if (Input.GetKeyDown(KeyCode.W))
            {
                m_input.Set(m_input.x, 1f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                m_input.Set(-1f, m_input.y);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                m_input.Set(1f, m_input.y);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (m_rb == null)
                return;

            float horizontalVelocity = 0f;

            if (m_input.x != 0)
            {
                horizontalVelocity = m_playerConfig.PlayerHorizontalSpeed * m_input.x;
            }

            if (m_input.y != 0 && m_isGrounded)
            {
                m_rb.AddForce(Vector2.up * m_playerConfig.PlayerJumpHeight, ForceMode2D.Impulse);
            }

            m_rb.velocity = new Vector2(horizontalVelocity, m_rb.velocity.y);

            m_input = Vector2.zero;
        }

        private bool CheckGrounded()
        {
            return Physics2D.BoxCast(transform.position, new Vector2(m_collider.size.x, 1f), 0f, Vector2.down, 5f, m_layersConfig.Ground.value);
        }

        public void Bump()
        {
            m_rb.velocity = m_rb.velocity.SetY(0f);
            m_rb.AddForce(Vector2.up * m_playerConfig.PlayerJumpHeight, ForceMode2D.Impulse);
        }
    }
}