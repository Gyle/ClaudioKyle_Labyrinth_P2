using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public string nextLevelName;

    void OnTriggerEnter(Collider collidee)
    {
        // if player reached goal, load next level
        if(collidee.gameObject.name == "Player")
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
