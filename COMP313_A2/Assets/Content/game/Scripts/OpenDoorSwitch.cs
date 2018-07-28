using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.InteractiveTutorials;

public class OpenDoorSwitch : MonoBehaviour
{
    public Door[] doors;
    public float delayInSeconds;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<IPlayerAvatar>() != null)
        {
            StopAllCoroutines();
            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(delayInSeconds);
		
		for(int i = 0; i < doors.Length; i++){
			doors[i].Open();
		}
		
//        door.Open();
    }
}
