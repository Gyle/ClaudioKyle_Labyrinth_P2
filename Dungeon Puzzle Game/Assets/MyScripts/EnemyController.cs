using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    
    public GameObject[] waypoints;      // patrol waypoints for enemy
    public NavMeshAgent agent;  // This object controls the movement
    public float initialWaitTime;       // How long the enemy will wait at waypoint

    private float currentWaitTime;      // How long the enemy has been waiting
    private int index;          // current waypoint index
    private GameObject wp;      // current waypoint game object

    // Use this for initialization
    void Start () {
        index = 0;
        wp = waypoints[index++];
        currentWaitTime = initialWaitTime;
    }
	
	// Update is called once per frame
	void Update () {
        // If it is time to move, move enemy
        if (currentWaitTime <= 0)
        {
            agent.SetDestination(wp.transform.position);    // Move enemy to next location
            currentWaitTime = initialWaitTime;              // Reset timer
            Update_Next_Waypoint();                         // update next waypoint
            
        }
        // It is not time to move so decrement timer
        else
        {
            currentWaitTime -= Time.deltaTime;
        }
	}

    void Update_Next_Waypoint()
    {
        if (index >= waypoints.Length)
        {
            index = 0;
        }

        wp = waypoints[index++];
    }

}
