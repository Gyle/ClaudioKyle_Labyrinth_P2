﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    
    public GameObject[] waypoints;      // patrol waypoints for enemy
    public NavMeshAgent agent;  // This object controls the movement
    public GameObject player;           // Player reference for chasing
    public float initialWaitTime;       // How long the enemy will wait at waypoint
    public bool moveOnEvent;   // if true, enemy is stationary until event door opens

    private float currentWaitTime;      // How long the enemy has been waiting
    private int index;          // current waypoint index
    private GameObject wp;      // current waypoint game object
    private bool agro;          // if true, it chases player


    // Use this for initialization
    private void Start () {
        index = 0;
        agro = false;
        wp = waypoints[index++];
        currentWaitTime = initialWaitTime;
    }

    // Update is called once per frame
    private void Update () {
        // base case
        if (moveOnEvent)
        {
            return;
        }
        // check if enemy is angry
        if(agro){
            Chase_Player();
        }
        else{
            Patrol_Area();
        }

	}

    private void Chase_Player(){
        // move to player's curent position
        agent.SetDestination(player.transform.position);

        // use path status check if path is partial, meaning cannot reach destination
        if (Path_Is_Partial("agro")){
            agro = false; // turn off agro as cannot reach player
        }
    }

    private void Patrol_Area(){
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
            // check if enemy path dynamically changed to partial
            if (Path_Is_Partial("patrol"))
            {
                print("path is partial. update to next valid waypoint");
                // partial path, so move to next waypoint
                currentWaitTime = initialWaitTime;
                Update_Next_Waypoint();
            }
            else
            {
                // decrement timer
                currentWaitTime -= Time.deltaTime;
            }
        }
    }

    // update the next waypoint for the agent to use for navigation
    private void Update_Next_Waypoint()
    {
        if (index >= waypoints.Length)
        {
            index = 0;
        }

        wp = waypoints[index++];
    }

    // if the player enters it's trigger collider, change to agro mode
    private void OnTriggerEnter(Collider entity)
    {

        if(entity.gameObject.name == "Player"){
            agro = true;
        }
    }

    // returns if path is partial (not possible to reach destination)
    private bool Path_Is_Partial(string mode){
        
        // compute path status
        NavMeshPath path = new NavMeshPath();
        Vector3 destination = (mode == "agro") ? player.transform.position : wp.transform.position;
        agent.CalculatePath(destination, path);

        // if path is patrial, return true.
        return path.status == NavMeshPathStatus.PathPartial;
    }

}
