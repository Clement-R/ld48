using UnityEngine;

namespace Game
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform m_handAnchor;
        [SerializeField] private WeaponBehavior m_baseWeapon;
        [SerializeField] private float m_throwHorizontalSpeed = 40f;
        [SerializeField] private float m_throwVerticalSpeed = 20f;

        private WeaponBehavior m_currentWeapon = null;

        private void Start()
        {
            EquipWeapon(m_baseWeapon);
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

        public void EquipWeapon(WeaponBehavior p_weapon)
        {
            ThrowCurrent();
            m_currentWeapon = Instantiate(p_weapon, m_handAnchor);
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