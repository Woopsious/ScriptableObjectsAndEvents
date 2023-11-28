using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUi : MonoBehaviour
{
	public static PlayerInventoryUi Instance;


	public GameObject ItemUiPrefab;
	public List<GameObject> InventorySlots = new List<GameObject>();

	public void Start()
	{
		Instance = this;
		gameObject.SetActive(false);
	}
	public void HideShowInventory()
	{
		if (gameObject.activeInHierarchy)
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
	}

	public void AddItemToUi()
	{

	}
}