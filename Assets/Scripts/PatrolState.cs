using UnityEngine;

namespace M2
{
    public class PatrolState : IEnemyState
    {
        private EnemyAI m_Enemy;
        private int m_NextWayPoint;
        
        public PatrolState(EnemyAI enemy)
        {
            m_Enemy = enemy;
        }
        
        public void UpdateState()
        {
            m_Enemy.spotLight.color = Color.green;
            
            m_Enemy.navMeshAgent.destination = m_Enemy.wayPoints[m_NextWayPoint].position;
            
            if (m_Enemy.navMeshAgent.remainingDistance <= m_Enemy.navMeshAgent.stoppingDistance)
            {
                m_NextWayPoint = (m_NextWayPoint + 1) % m_Enemy.wayPoints.Length;
            }
        }
        
        public void Impact()
        {
            GoToAttackState();
        }
        
        public void GoToAlertState()
        {
            m_Enemy.navMeshAgent.isStopped = true;
            m_Enemy.currentState = m_Enemy.alertState;
        }
        
        public void GoToAttackState()
        {
            m_Enemy.navMeshAgent.isStopped = true;
            m_Enemy.currentState = m_Enemy.attackState;
        }
        
        public void GoToPatrolState() {}

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                GoToAlertState();
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
                GoToAlertState();
        }
        
        public void OnTriggerExit(Collider other) {}
    }
}
