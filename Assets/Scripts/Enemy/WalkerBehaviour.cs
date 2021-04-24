using UnityEngine;

using Game.Shared;

namespace Game
{
    public class WalkerBehaviour : Enemy
    {
        [SerializeField] private int m_wallCastDistance;
        [SerializeField] private int m_edgeCastDistance;

        private LayersConfig m_layersConfig;

        protected override void Start()
        {
            base.Start();
            m_layersConfig = ConfigsManager.Instance.Get<LayersConfig>();
        }

        protected override void Update()
        {
            base.Update();

            var changeDirection = false;
            Debug.DrawLine(transform.position, transform.position + transform.right * m_wallCastDistance, Color.red);

            if (Physics2D.Raycast(transform.position, transform.right, m_wallCastDistance, m_layersConfig.Ground.value))
            {
                // Wall detected in front
                changeDirection = true;
            }

            if (!changeDirection)
            {
                // Edge detection
                var start = transform.position + transform.right * m_edgeCastDistance;
                Debug.DrawLine(transform.position, start, Color.blue);
                Debug.DrawLine(start, start + Vector3.down * 4f, Color.cyan);

                if (!Physics2D.Raycast(transform.position + transform.right * m_edgeCastDistance, Vector2.down, 4f, m_layersConfig.Ground.value))
                {

                    changeDirection = true;
                }
            }

            if (changeDirection)
            {
                transform.right *= -1f;
            }
        }

        protected override void FixedUpdate()
        {
            // Move forward
            m_rb.velocity = transform.right * m_speed;
        }
    }
}