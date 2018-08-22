using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Script is attached to player. The player notifies DeathEventManager.cs when they die to signal
 the handler it is time to do the restart level sequence
*/
public class DeathController : MonoBehaviour {

    public GameObject deathHandler; // reference to the death handler game object which is invisble and handles death sequence

    // notify the DeathEventHandler class that the player died, so we much restart level
    private void OnTriggerEnter(Collider entity)
    {
        if (entity.gameObject.tag == "Enemy")
        {
            // make troll animate hitting player
            Animator animator = entity.GetComponentInParent<Animator>();
            animator.SetTrigger("attack");

            deathHandler.GetComponent<DeathEventManager>().Player_Died();
        }

        if (entity.gameObject.tag == "PitFall")
        {
            deathHandler.GetComponent<DeathEventManager>().Player_Died();
        }
    }
    
}
