using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public void CleanUpDeadEntitiesCoroutine(GameObject obj)
	{
		StartCoroutine(CleanUpDeadEntities(obj));
	}
	public IEnumerator CleanUpDeadEntities(GameObject obj)
	{
		obj.SetActive(false);
		yield return new WaitForSeconds(5);
		Destroy(obj);
	}
}
