using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GameObjectCollidor : MonoBehaviour {
	
	public delegate void EventHandler(GameObjectCollidor sender);

	public static event EventHandler playerEnteredCollision = null;
	public static event EventHandler playerExitedCollision = null;
	public static event EventHandler playerEnteredTrigger = null;
	public static event EventHandler playerExitedTrigger = null;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponentInChildren<PlayerAvatar>() != null)
		{
			if (playerEnteredCollision != null)
				playerEnteredCollision(this);
				print("entered goal");
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.GetComponentInChildren<PlayerAvatar>() != null)
		{
			if (playerExitedCollision != null)
				playerExitedCollision(this);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<PlayerAvatar>() != null)
		{
			if (playerEnteredTrigger != null)
			{
				playerEnteredTrigger(this);
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.GetComponentInChildren<PlayerAvatar>() != null)
		{
			if (playerExitedTrigger != null)
				playerExitedTrigger(this);
		}
	}

	
}
