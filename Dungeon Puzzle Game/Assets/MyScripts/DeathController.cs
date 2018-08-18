using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathController : MonoBehaviour {

    public GameObject deathHandler;

    void OnTriggerEnter(Collider entity)
    {
        if (entity.gameObject.tag == "Enemy")
        {
            print("die");
            deathHandler.GetComponent<DeathEventManager>().Player_Died();
            print("live");
        }
    }
    
}
