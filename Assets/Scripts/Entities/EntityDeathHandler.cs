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
	}
	public bool Dead(int currentHealth)
	{
		if (currentHealth >= 0)
			return true;
		else
			return false;
	}
}
