using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpDeadEntities : MonoBehaviour
{
	public void OnEntityDeath()
	{
		//play death sound
		//play death animation
		//in MP call RPC to clean up 

		//Destroy will destroy the prefab so its disabled for now (unity throws an error anyway)
		//Destroy(gameObject);
	}
	public bool Dead(int currentHealth)
	{
		if (currentHealth >= 0)
			return true;
		else
			return false;
	}
}
