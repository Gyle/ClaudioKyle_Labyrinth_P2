using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 This script is attached to the stairs object at the goal door.
 When the player collides with the stair box collider, load
 next level
*/

public class LevelLoader : MonoBehaviour {

    public string nextLevelName;    // scene name of next level to load

    private void OnTriggerEnter(Collider collidee)
    {
        // if player reached goal, load next level
        if(collidee.gameObject.name == "Player")
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

   
}
