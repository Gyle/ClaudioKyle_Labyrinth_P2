using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 This script is attached to a stand alone game object. The role of this script is to 
 handle the death effects of notifying ThirdPersonUserControl.js that player cannot 
 move once dead. Also, create fade effect indicating to player they died. Finally, 
 it reloads current scene to restart level
*/
public class DeathEventManager : MonoBehaviour {
    public Image img;           // white image that creates fade effect by increase alpha and transition to black color
    public GameObject player;   // reference to player to notify ThirdPersonUserControl.js they died
    private Scene scene;        // current scene
    private bool fading;        // if the scene in the middle of fading image
    private bool showImage;     // if the image is on screen
    private bool playerAlive;   // if the player died to determine when to restart level
    private float transition;   // alpha value
    private float fadeDuration; // how long fade takes until fully faded
   

    // Use this for initialization
    void Start () {
        scene = SceneManager.GetActiveScene();
        playerAlive = true;
        showImage = false;
        //Fade_Image(false, 1.5f); // begin with fade out
    }
	
	// Update is called once per frame
	void Update () {
        // do not do anything if we do not need to fade the image
        if (!fading)
        {
            return;
        }

        // we are mid fade, so update transition value for fade effect
        Update_Transition();

        // lerping from white without alpha to white with alpha
        img.color = Color.Lerp(new Color(1, 1, 1, 0), Color.black, transition);

        // if done fading, reloading scene and set bool flag for base case
        Check_If_Done_Fading();
    }

    // player will notify this class that it is time to restart level
    public void Player_Died()
    {
        playerAlive = false;
        // long argument because of standard library namespace issue
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().Set_Dead();
        Fade_Image(true, 1.5f);
    }

    // update the trasition value according to affect the image alpha correctly for fade effect
    private void Update_Transition()
    {
        if (showImage)
        {
            transition = transition + Time.deltaTime * (1 / fadeDuration);

        }
        else
        {
            transition = transition - Time.deltaTime * (1 / fadeDuration);
        }
    }

    // Check to see if fading is over to reload the current scene
    private void Check_If_Done_Fading()
    {
        if (transition > 1 || transition < 0)
        {
            fading = false;

            // reload level if player died
            if (!playerAlive)
            {
                playerAlive = true;
                SceneManager.LoadScene(scene.name);
            }
           
        }
    }

    // fades the image on the UI 
    private void Fade_Image(bool imageShowing, float duration)
    {
        fadeDuration = duration;
        showImage = imageShowing;
        fading = true;
        if (showImage)
        {
            transition = 0;
        }
        else
        {
            transition = 1;
        }

    }


}
