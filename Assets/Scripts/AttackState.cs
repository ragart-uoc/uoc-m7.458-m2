using UnityEngine;

namespace M2
{
    public class AttackState : IEnemyState
    {
        private EnemyAI m_Enemy;
        private float m_ActualTimeBetweenShots;
        
        public AttackState(EnemyAI enemy)
        {
            m_Enemy = enemy;
        }
        
        public void UpdateState()
        {
            m_Enemy.spotLight.color = Color.red;
            m_ActualTimeBetweenShots += Time.deltaTime;
        }
        
        public void Impact()
        {
            GoToAttackState();
        }
        
        public void GoToAttackState() {}
        public void GoToPatrolState() {}
        
        public void GoToAlertState()
        {
            m_Enemy.currentState = m_Enemy.alertState;
        }
        
        public void OnTriggerEnter(Collider other) {}
        
        public void OnTriggerStay(Collider other)
        {
            var lookDirection = other.transform.position - m_Enemy.transform.position;
            
            m_Enemy.transform.rotation = Quaternion.FromToRotation(
                Vector3.forward,
                new Vector3(lookDirection.x, 0.0f, lookDirection.z));

            if (m_ActualTimeBetweenShots >= m_Enemy.timeBetweenShots)
            {
                m_ActualTimeBetweenShots = 0.0f;
                m_Enemy.audioSource.Play();
                other.GetComponent<Shooter>().Hit(m_Enemy.damageForce);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            GoToAlertState();
        }
    }
}
