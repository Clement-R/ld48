using System;
using System.Collections;

using UnityEngine;

using Game.Shared;

namespace Game
{

    public class PlayerControler : PausableMonoBehaviour
    {
        public Action OnJump;
        public bool Grounded => m_isGrounded;
        public Vector2 CurrentInput => m_input;

        private PlayerManager m_playerManager = null;
        private PlayerConfig m_playerConfig = null;
        private LayersConfig m_layersConfig;

        private Rigidbody2D m_rb;
        private BoxCollider2D m_collider;
        private Health m_health;
        private Vector2 m_input;
        private bool m_isGrounded = true;
        private bool m_initialized = false;

        protected async void Awake()
        {
            await ConfigsManager.WaitForInstance();

            m_rb = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<BoxCollider2D>();
            m_health = GetComponent<Health>();

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

            if (!m_initialized || !m_health.Alive)
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

            if (m_rb == null || !m_health.Alive)
                return;

            float horizontalVelocity = 0f;

            // Facing direction
            if (m_input.x != 0)
                transform.right = Vector2.right * m_input.x;

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

        public void KnockBack(Vector2 p_direction)
        {
            m_rb.velocity = Vector2.zero;

            GetComponent<Collider2D>().enabled = false;

            var force = Vector2.up * m_playerConfig.PlayerJumpHeight + p_direction * m_playerConfig.PlayerHorizontalSpeed;
            m_rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}