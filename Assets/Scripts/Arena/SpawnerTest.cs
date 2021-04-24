using UnityEngine;

namespace Game
{
    public class SpawnerTest : MonoBehaviour
    {
        [SerializeField] private Spawner m_spawner;
        [SerializeField] private GameObject m_walker;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_spawner.Spawn(m_walker);
            }
        }
    }
}