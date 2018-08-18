using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathEventManager : MonoBehaviour {
    public Image img;
    public GameObject player;
    private Scene scene;
    private bool fading;
    private bool showImage;
    private bool playerAlive;
    private float transition;
    private float fadeDuration;
   

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
