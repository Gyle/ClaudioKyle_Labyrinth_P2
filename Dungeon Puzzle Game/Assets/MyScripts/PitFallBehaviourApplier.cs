using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 This script is attached to trap doors. A trap door will have a box collider and a nav mesh obstacle 
 for carving out the nav mesh, depending on position. When trap door is released, both 
 box collider and nav mesh obstacle move down to squish the enemy with a trigger box. If the enemy
 touches this trigger, apply physics to the enemy by disabling nav mesh and applying rigid body attributes.
*/
public class PitFallBehaviourApplier : MonoBehaviour {

    private void OnTriggerEnter(Collider collidee)
    {
        // check if enemy fell for trap door 
        if (collidee.gameObject.tag == "EnemyBody")
        {
            // get EnemyController to update flag the enemy fell off map
            EnemyController enemyController = collidee.gameObject.GetComponentInParent<EnemyController>();

            // get animator to stop it
            Animator animator = collidee.gameObject.GetComponentInParent<Animator>();

            // get required objects for applying gravity
            NavMeshAgent enemyAgent = collidee.GetComponentInParent<NavMeshAgent>();
            Rigidbody enemyRb = collidee.gameObject.GetComponentInParent<Rigidbody>();

            // turn off animation
            animator.StopPlayback();

            // apply gravity to the victim enemy
            enemyAgent.enabled = false;
            enemyRb.isKinematic = false;

            // apply flag to stop fallen enemy from trying to use NavMeshAgent functions
            enemyController.Set_Fell_Off_Map();
        }
    }

}
