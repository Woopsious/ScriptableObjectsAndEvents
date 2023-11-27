using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CleanUpDeadEntities : MonoBehaviour
{
	[Serializable]
	public class EntityDeathEvent : UnityEvent<Vector3> { }
	public EntityDeathEvent onEntityDeath;

	//invoked from event
	public void OnRecieveDamage(int currentHealth, int maxHealth)
	{
		if (CheckIfDead(currentHealth, maxHealth))
		//play death sound
		//play death animation
		//in MP call RPC to clean up
		Destroy(gameObject);
	}
	public bool CheckIfDead(int currentHealth, int maxHealth)
	{
		if (currentHealth <= 0)
			return true;
		else return false;
	}
}
