using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    
    public GameObject[] waypoints;      // patrol waypoints for enemy
    public NavMeshAgent agent;  // This object controls the movement
    public GameObject player;           // Player reference for chasing
    public float initialWaitTime;       // How long the enemy will wait at waypoint

    private float currentWaitTime;      // How long the enemy has been waiting
    private int index;          // current waypoint index
    private GameObject wp;      // current waypoint game object
    private bool agro;          // if true, it chases player

    // Use this for initialization
    void Start () {
        index = 0;
        agro = false;
        wp = waypoints[index++];
        currentWaitTime = initialWaitTime;
    }
	
	// Update is called once per frame
	void Update () {
        // check if enemy is angry
        if(agro){
            Chase_Player();
        }
        else{
            Patrol_Area();
        }

	}

    void Chase_Player(){
        // move to player's curent position
        agent.SetDestination(player.transform.position);

        // compute path status
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(player.transform.position, path);

        // use path status check if path is partial, meaning cannot reach destination
        if (path.status == NavMeshPathStatus.PathPartial){
            agro = false; // turn off agro as cannot reach player
        }
    }

    void Patrol_Area(){
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

    void OnTriggerEnter(Collider entity)
    {

        if(entity.gameObject.name == "Player"){
            Debug.Log("agro");
            agro = true;
        }
    }

}
