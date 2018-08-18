using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour {
	public bool useOpenRange;//uses the gameObjects door script to determine when to trigger - set the door scripts openRange to change the distance
	//public bool twoWayDoor; //only works for hinged doors
	public GameObject UI;// the Canvas where our UIText lives - will set this to active
	public Text UIText; //what text gameobject we want to control color/size/placement of our text
	string openMessage = "Open with "; //set here the text we want to appear before the button text
	string closeMessage = "Close with ";//set here the text we want to appear before the button text
	public bool oneButton; // will only use one button to open and close the door - this will use the openButton only
	string useMessage = "Use ";
	public string openButton = "v"; //choose a keycode we would like for opening the door
	public string closeButton = "c";//choose a keycode we would like for closing the door
	
	// This variable represents which switch they are in range of using
	GameObject vicinitySwitch;
    // debug boolean
    bool debugMode = false;
	
	float openState;
	float state;
	float UIDelay;
	float UITimer;
	float delay;
	float timer;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
        // enter debug mode, which opens all doors at the moment.
        if (Input.GetMouseButtonDown(1))
        {
            debugMode = !debugMode;
            Debug.Log("DebugMode Toggle: " + debugMode);
        }
		if (useOpenRange == true) {
			for(int i = 0; i < gameObject.GetComponent<Door>().activateTargets.Length; i++){
				if (gameObject.GetComponent<Door>().activateTargets[i] != null) {
					if (Vector3.Distance (gameObject.GetComponent<Door>().activateTargets[i].transform.position, gameObject.GetComponent<Door>().player.transform.position) <= gameObject.GetComponent<Door>().openRange) {
						vicinitySwitch = gameObject.GetComponent<Door>().activateTargets[i];
						state = 1;
						break;

					} else {
						state = 0;
					}
				} else
				{
					if (Vector3.Distance (transform.position, gameObject.GetComponent<Door>().player.transform.position) <= gameObject.GetComponent<Door>().openRange) {
						state = 1;
						break;
					} else
					{
						state = 0;
					}
				}
			}
		}
		if (state == 0) {
			if (UI != null)
			{
					UI.SetActive (false);
			}

		}

        // begin door logic if in range of trigger or Debug mdoe is on
        if (state == 1 || debugMode)
		{
			if (UI != null)
			{
				if (gameObject.GetComponent<Door> ().isOpen == true) {
					if (oneButton == false) {
						UIText.text = closeMessage + closeButton;
					}
				} else {
					if (oneButton == true) {
						UIText.text = useMessage + openButton;
					} else {
						UIText.text = openMessage + openButton;
					}
				}
				if (useOpenRange == true) {
					for(int i = 0; i < gameObject.GetComponent<Door>().activateTargets.Length; i++){
						if (gameObject.GetComponent<Door>().activateTargets[i] != null) {
							if (Vector3.Distance (gameObject.GetComponent<Door>().activateTargets[i].transform.position, gameObject.GetComponent<Door>().player.transform.position) <= gameObject.GetComponent<Door>().openRange) {
								if (UITimer <= Time.time)
								{
									UI.SetActive (true);
								}
							}
							else
							{
									UI.SetActive (false);
							}
						}
					}
				}

			}

			//on button press do this stuff
			if (delay == 0) {
//				if (Input.GetKey (openButton)) {
				// 0  means primary mouse button
				if (Input.GetMouseButtonDown(0)) {
					// boolean to notify program to only open/close door if this is true
					// in order for this to be true, the player must be in range of the
					// switch that they clicked on
					bool correctInteraction = false;
						
					// these variable stores where the user clicked
					RaycastHit mouse;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					
					if(Physics.Raycast(ray, out mouse, 100.0f)){
						if(mouse.transform != null){
							//print(mouse.transform.gameObject);
							
							// check if the user clicked on the switch they are close to
                            if(mouse.transform.gameObject == vicinitySwitch){
                                //print("player in vicinity");
								correctInteraction = true;
							}
						}
					}
					if (UI != null)
					{
						UI.SetActive (false);
					}

					// check if player within the rules of using switches before taking actions.
                    if(correctInteraction || debugMode){
						if (oneButton == true)
						{
							if (openState == 0) {
								gameObject.GetComponent<Door>().triggerOpen = true;
								openState = 1;
							}
							if (openState == 2) {
								gameObject.GetComponent<Door>().triggerClose = true;
								openState = 0;
							}
						} else {
							gameObject.GetComponent<Door> ().triggerOpen = true;
						}
					}
					timer = Time.time + .4f;
					UITimer = Time.time + 1.2f;
					delay = 1;
				}
				if (oneButton == false) {
					if (Input.GetKey (closeButton)) {
						if (UI != null) {
							UI.SetActive (false);
						}
						gameObject.GetComponent<Door>().triggerClose = true;
						timer = Time.time + .4f;
						UITimer = Time.time + 1.2f;
						delay = 1;
					}
				}
			}
			if (delay == 1) {
				if (timer <= Time.time)
				{
					if (openState == 1){openState = 2;}
					delay = 0;
				}
			}


		}
	}
}
