using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    
    public NavMeshAgent agent;  // This object controls the movement
    public float speed;         // How fast the enemy can walk
    public float initialWaitTime;   // How long the enemy will wait at random spot
    private float currentWaitTime;    // How long the enemy has been waiting

    // Use this for initialization
    void Start () {
        currentWaitTime = initialWaitTime;
    }
	
	// Update is called once per frame
	void Update () {
        // If it is time to move, move enemy
		if (currentWaitTime <= 0)
        {
            Vector3 randomSpot = Generate_Random_Position();
            agent.SetDestination(randomSpot);   // Move enemy to random location
            currentWaitTime = initialWaitTime;  // Reset timer
        }
        // It is not time to move so decrement timer
        else
        {
            currentWaitTime -= Time.deltaTime;
        }
	}

    Vector3 Generate_Random_Position()
    {
        float x = Random.Range(-10, 10);
        float y = 1.210377f;
        float z = Random.Range(-10, 10);
        return new Vector3(x, y, z);
    }

}
