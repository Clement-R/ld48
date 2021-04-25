using UnityEngine;

namespace Game
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform m_handAnchor;
        [SerializeField] private Weapon m_baseWeapon;
        [SerializeField] private float m_throwHorizontalSpeed = 40f;
        [SerializeField] private float m_throwVerticalSpeed = 20f;

        private Weapon m_currentWeapon = null;

        private void Start()
        {
            EquipWeapon(m_baseWeapon, true);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.H))
            {
                m_currentWeapon.TryToShoot();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                ThrowCurrent();
            }
        }

        public void EquipWeapon(Weapon p_weapon, bool p_isTemplate = false)
        {
            ThrowCurrent();

            if (p_isTemplate)
            {
                m_currentWeapon = Instantiate(p_weapon);
            }
            else
            {
                m_currentWeapon = p_weapon;
            }

            m_currentWeapon.Equipped();

            m_currentWeapon.transform.SetParent(m_handAnchor);
            m_currentWeapon.transform.localPosition = Vector3.zero;
            m_currentWeapon.transform.localRotation = Quaternion.identity;
        }

        private void ThrowCurrent()
        {
            if (m_currentWeapon == null)
                return;

            m_currentWeapon.transform.SetParent(null);
            m_currentWeapon.transform.rotation = Quaternion.identity;
            m_currentWeapon.transform.localScale = new Vector3(-transform.right.x, 1f, 1f);

            m_currentWeapon.Throw((Vector2) transform.right * m_throwHorizontalSpeed + Vector2.up * m_throwVerticalSpeed);
            Destroy(m_currentWeapon.gameObject, 25f);

            m_currentWeapon = null;
        }

        [ContextMenu("Equip weapon")]
        private void Equip()
        {
            EquipWeapon(m_baseWeapon);
        }
    }
}