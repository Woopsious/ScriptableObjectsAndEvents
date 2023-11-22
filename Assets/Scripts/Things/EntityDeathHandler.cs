using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpDeadEntities : MonoBehaviour
{
	public void OnRecieveDamage(int currentHealth)
	{
		if (!Dead(currentHealth)) return;

		//play death sound
		//play death animation
		//in MP call RPC to clean up 
		gameObject.SetActive(false);
		StartCoroutine(DestroyIn());
	}
	public IEnumerator DestroyIn()
	{
		yield return new WaitForSeconds(10);
		Destroy(gameObject);
	}
	public bool Dead(int currentHealth)
	{
		if (currentHealth >= 0)
			return true;
		else
			return false;
	}
}
