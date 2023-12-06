using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryUi : MonoBehaviour
{
	public static PlayerInventoryUi Instance;

	public GameObject ItemUiPrefab;
	public List<GameObject> InventorySlots = new List<GameObject>();

	public GameObject weaponEquipmentSlot;

	public GameObject helmetEquipmentSlot;
	public GameObject chestpieceEquipmentSlot;
	public GameObject legsEquipmentSlot;

	public GameObject consumableSlotOne;
	public GameObject consumableSlotTwo;

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
}
