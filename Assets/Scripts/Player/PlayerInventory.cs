using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;

	public List<DroppedItems> playerInventory = new List<DroppedItems>();

	public void Start()
	{
		Instance = this;
	}

	public void AddItemToPlayerInventory(DroppedItems item)
	{
		playerInventory.Add(item);

		foreach (DroppedItems itemInInventry in playerInventory)
		{
			if (itemInInventry.itemType == DroppedItems.ItemType.isWeapon)
				Debug.Log("items in Inventory: " + itemInInventry.weapon.baseDamage);
		}
	}
	public void RemoveItemToPlayerInventory(DroppedItems item)
	{

	}
}
