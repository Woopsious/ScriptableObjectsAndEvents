using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public List<GameObject> weaponDataBase = new List<GameObject>();

	public void Start()
	{
		Instance = this;
	}
}
