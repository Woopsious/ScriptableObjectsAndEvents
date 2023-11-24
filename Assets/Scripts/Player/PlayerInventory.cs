using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;

	public GameObject weaponContainer;

	public List<InventoryItem> playerInventory = new List<InventoryItem>();

	public void Start()
	{
		Instance = this;
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			EquipRandomWeaponInInventroy();
		}
	}

	public void AddItemToPlayerInventory(Items item)
	{
		InventoryItem itemData = new();

		if (item.itemType == Items.ItemType.isWeapon)
		{
			itemData = new()
			{
				itemName = item.itemName,
				itemImage = item.itemImage,
				itemLevel = item.itemLevel,

				itemType = (InventoryItem.ItemType)item.itemType,
				rarity = (InventoryItem.Rarity)item.rarity,

				weaponBaseRef = item.weaponBaseRef,
				damage = (int)(item.weaponBaseRef.baseDamage * item.statModifier),
				bonusMana = (int)(item.weaponBaseRef.baseBonusMana * item.statModifier),

				maxStackCount = item.weaponBaseRef.MaxStackCount,
				currentStackCount = item.currentStackCount
			};
		}

		playerInventory.Add(itemData);
	}
	public void RemoveItemFromPlayerInventory(Items item)
	{

	}

	public void EquipRandomWeaponInInventroy()
	{
		for (int i = 0; i < 1000; i++)
		{
			int index = GetRandomNumber();

			if (playerInventory[index].itemType == InventoryItem.ItemType.isWeapon)
			{
				GameObject go;
				switch (playerInventory[index].weaponBaseRef.baseDamage)
				{
					case 35:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
					case 25:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
					case 8:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
					case 30:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
				}
				return;
			}
		}
	}
	public void SetUpWeaponObj(GameObject go, InventoryItem inventoryItem)
	{
		Weapons weapon = go.GetComponent<Weapons>();

		weapon.itemLevel = inventoryItem.itemLevel;
		weapon.itemType = (Items.ItemType)inventoryItem.itemType;
		weapon.rarity = (Items.Rarity)inventoryItem.rarity;
		weapon.weaponBaseRef = inventoryItem.weaponBaseRef;
		weapon.consumableBaseRef = inventoryItem.consumableBaseRef;

		go.GetComponent<Weapons>().isEquippedByPlayer = true;
		go.transform.localPosition = Vector3.zero;
		Destroy(go.GetComponent<Interactables>()); //on dropped item spawn the interactables script will be added
	}
	public int GetRandomNumber()
	{
		return Random.Range(0, playerInventory.Count);
	}
}
