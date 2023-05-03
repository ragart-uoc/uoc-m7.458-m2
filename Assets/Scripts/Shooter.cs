using UnityEngine;

namespace M2
{
    public class Shooter : MonoBehaviour
    {
        public GameObject decalPrefab;
        public AudioSource fireSound;

        private GameObject[] m_Decals;
        private int m_DecalIndex;

        private void Start()
        {
            m_Decals = new GameObject[10];
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                if (Physics.Raycast(Camera.main.ViewportPointToRay(
                        new Vector3(0.5f, 0.5f, 0.0f)), out var hit))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                    {
                        Destroy(m_Decals[m_DecalIndex]);
                        m_Decals[m_DecalIndex] = Instantiate(decalPrefab, hit.point + hit.normal * 0.01f,
                            Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                        m_DecalIndex = (m_DecalIndex + 1) % m_Decals.Length;
                    }
                    if (hit.transform.CompareTag("Enemy"))
                        hit.transform.gameObject.GetComponent<EnemyAI>().Hit(10.0f);
                }
                fireSound.Play();
            }
        }
        
        public void Hit(float damage)
        {
            Debug.Log("Hit! Damage: " + damage);
        }
    }
}