using UnityEngine;
using UnityEngine.AI;

namespace M2
{
    public class EnemyAI : MonoBehaviour
    {
        [HideInInspector]
        public PatrolState patrolState;
        
        [HideInInspector]
        public AlertState alertState;
        
        [HideInInspector]
        public AttackState attackState;
        
        [HideInInspector]
        public IEnemyState currentState;
        
        [HideInInspector]
        public NavMeshAgent navMeshAgent;
        
        public Light spotLight;
        public float life = 100f;
        public float timeBetweenShots = 1.0f;
        public float damageForce = 10.0f;
        public float rotationTime = 3.0f;
        public float shootHeight = 0.5f;
        public Transform[] wayPoints;
        public AudioSource audioSource;

        private void Start()
        {
            patrolState = new PatrolState(this);
            alertState = new AlertState(this);
            attackState = new AttackState(this);
            
            currentState = patrolState;
            
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        private void Update()
        {
            currentState.UpdateState();
            
            if (life <= 0)
                Destroy(gameObject);
        }
        
        public void Hit(float damage)
        {
            life -= damage;
            currentState.Impact();
            Debug.Log("Enemy hit! Life: " + life);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            currentState.OnTriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            currentState.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            currentState.OnTriggerExit(other);
        }
    }
}
