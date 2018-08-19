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
    private bool moveOnEvent;   // if true, enemy is stationary until event door opens

    // Use this for initialization
    void Start () {
        index = 0;
        agro = false;
        wp = waypoints[index++];
        currentWaitTime = initialWaitTime;
        moveOnEvent = false;
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

        // use path status check if path is partial, meaning cannot reach destination
        if (Path_Is_Partial()){
            agro = false; // turn off agro as cannot reach player
        }
    }

    void Patrol_Area(){
        print("patrol time");
        // If it is time to move, move enemy
        if (currentWaitTime <= 0)
        {
            agent.SetDestination(wp.transform.position);    // Move enemy to next location
            currentWaitTime = initialWaitTime;              // Reset timer
            Update_Next_Waypoint();                         // update next waypoint

        }
        // It is not time to move
        else
        {
            Debug.Log(Path_Is_Partial());
            // check if enemy path dynamically changed to partial
            if (Path_Is_Partial() && moveOnEvent)
            {
                print("path is partial. update to next valid waypoint");
                // partial path, so move to next non partial path waypoint
                Set_To_Next_Valid_Waypoint();
                currentWaitTime = initialWaitTime;
                agent.SetDestination(wp.transform.position);
            }
            else
            {
                // decrement timer
                currentWaitTime -= Time.deltaTime;
            }
        }
    }


    private void Set_To_Next_Valid_Waypoint(){
        while(Path_Is_Partial())
        {
            Update_Next_Waypoint();
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

    // returns if path is partial (not possible to reach destination)
    private bool Path_Is_Partial(){
        
        // compute path status
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(player.transform.position, path);

        // if path is patrial, return true.
        return path.status == NavMeshPathStatus.PathPartial;
    }

}
