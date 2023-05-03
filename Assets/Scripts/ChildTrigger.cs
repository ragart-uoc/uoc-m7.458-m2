using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M2
{
    public class ChildTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            transform.parent.GetComponent<EnemyAI>().currentState.OnTriggerEnter(other);
        }

        private void OnTriggerStay(Collider other)
        {
            transform.parent.GetComponent<EnemyAI>().currentState.OnTriggerStay(other);
        }

        private void OnTriggerExit(Collider other)
        {
            transform.parent.GetComponent<EnemyAI>().currentState.OnTriggerExit(other);
        }
    }
}
