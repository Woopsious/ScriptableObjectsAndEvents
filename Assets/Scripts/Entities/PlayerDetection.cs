using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
	//set in entitybehaviour script on start
	public EntityBehaviour entityBehaviourRef;

	public void SetBehaviourRef(EntityBehaviour behaviour)
	{
		entityBehaviourRef = behaviour;
	}

	//in Mp will need to be modified to handle multiple players
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerController>() == null) return;

		if (other.gameObject.GetComponent<PlayerController>())
			entityBehaviourRef.player = other.gameObject.GetComponent<PlayerController>();
	}
}
