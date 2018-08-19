using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathController : MonoBehaviour {

    public GameObject deathHandler;

    // notify the DeathEventHandler class that the player died, so we much restart level
    private void OnTriggerEnter(Collider entity)
    {
        if (entity.gameObject.tag == "Enemy")
        {
            deathHandler.GetComponent<DeathEventManager>().Player_Died();
        }
    }
    
}
