using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerController>() != null && GetComponent<DroppedItems>() != null)
			GetComponent<DroppedItems>().PickUpItem();
	}
}
