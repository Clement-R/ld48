using UnityEngine;

namespace Game
{
    public abstract class Enemy : PausableMonoBehaviour
    {
        [SerializeField] private int m_life;
        [SerializeField] private int m_speed;
    }
}