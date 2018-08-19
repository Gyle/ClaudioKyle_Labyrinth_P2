using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PitFallBehaviourApplier : MonoBehaviour {

    private void OnTriggerEnter(Collider collidee)
    {
        // check if enemy fell for trap door 
        if (collidee.gameObject.tag == "EnemyBody")
        {
            // get EnemyController to update flag the enemy fell off map
            EnemyController enemyController = collidee.gameObject.GetComponentInParent<EnemyController>();

            // get required objects for applying gravity
            NavMeshAgent enemyAgent = collidee.GetComponentInParent<NavMeshAgent>();
            Rigidbody enemyRb = collidee.gameObject.GetComponentInParent<Rigidbody>();
            
            // apply gravity to the victim enemy
            enemyAgent.enabled = false;
            enemyRb.isKinematic = false;

            // apply flag to stop fallen enemy from trying to use NavMeshAgent functions
            enemyController.Set_Fell_Off_Map();
        }
    }

}
