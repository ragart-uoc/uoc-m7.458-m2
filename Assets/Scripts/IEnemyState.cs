using UnityEngine;

namespace M2
{
    public interface IEnemyState
    {
        void UpdateState();
        void GoToAlertState();
        void GoToAttackState();
        void GoToPatrolState();
        void OnTriggerEnter(Collider other);
        void OnTriggerStay(Collider other);
        void OnTriggerExit(Collider other);
        void Impact();
    }
}
