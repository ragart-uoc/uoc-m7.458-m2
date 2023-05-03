using UnityEngine;

namespace M2
{
    public class AlertState : IEnemyState
    {
        private EnemyAI m_Enemy;
        private float m_CurrentRotationTime;
        
        public AlertState(EnemyAI enemy)
        {
            m_Enemy = enemy;
        }

        public void UpdateState()
        {
            m_Enemy.spotLight.color = Color.yellow;
            
            m_Enemy.transform.rotation *=
                Quaternion.Euler(0.0f, 360.0f * Time.deltaTime / m_Enemy.rotationTime, 0.0f);
            
            if (m_CurrentRotationTime >= m_Enemy.rotationTime)
            {
                m_CurrentRotationTime = 0.0f;
                GoToPatrolState();
            }
            else
            {
                if (Physics.Raycast(
                        new Ray(
                            new Vector3(m_Enemy.transform.position.x,
                                m_Enemy.transform.position.y + 0.5f,
                                m_Enemy.transform.position.z),
                            m_Enemy.transform.forward * 100f),
                        out var hit))
                {
                    if (hit.collider.CompareTag("Player"))
                        GoToAttackState();
                }
                m_CurrentRotationTime += Time.deltaTime;
            }
        }
        
        public void Impact()
        {
            GoToAttackState();
        }
        
        public void GoToAlertState() {}
        
        public void GoToAttackState()
        {
            m_Enemy.currentState = m_Enemy.attackState;
        }
        
        public void GoToPatrolState()
        {
            m_Enemy.navMeshAgent.isStopped = false;
            m_Enemy.currentState = m_Enemy.patrolState;
        }
        
        public void OnTriggerEnter(Collider other) {}
        public void OnTriggerStay(Collider other) {}
        public void OnTriggerExit(Collider other) {}
    }
}
