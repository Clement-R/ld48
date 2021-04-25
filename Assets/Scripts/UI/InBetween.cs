using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using DG.Tweening;

namespace Game
{
    public class InBetween : MonoBehaviour
    {
        [SerializeField] private float m_maxY;
        [SerializeField] private TilemapRenderer m_tilemapRenderer;
        [SerializeField] private Transform m_tilemap;

        public void StartEffect(Color p_color, float p_duration)
        {
            var tileMaterial = m_tilemapRenderer.material;
            tileMaterial.SetColor("_Color", p_color);

            m_tilemap.DOMoveY(m_maxY, p_duration, true).From(Vector2.zero);
        }
    }
}