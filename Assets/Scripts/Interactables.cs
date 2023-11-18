using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
	public bool isInteractable;
	private void OnTriggerEnter(Collider other)
	{
		if (!isInteractable)
			return;

		Debug.Log("collision");

		if (other.GetComponent<PlayerController>() != null && GetComponent<DroppedItems>() != null)
			GetComponent<DroppedItems>().PickUpItem();
	}
}
