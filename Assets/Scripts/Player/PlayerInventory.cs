using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;

	public List<InventoryItem> playerInventory = new List<InventoryItem>();	

	public void Start()
	{
		Instance = this;
	}
	public void Update()
	{

	}

	public void AddItemToPlayerInventory(Items item)
	{
		if (item.isStackable)
		{
			for (int i = 0; i < PlayerInventoryUi.Instance.InventorySlots.Count; i++)
			{
				GameObject inventroySlot = PlayerInventoryUi.Instance.InventorySlots[i];
				InventoryItem itemInSlot;
				if (inventroySlot.GetComponentInChildren<InventoryItem>() != null)
				{
					itemInSlot = inventroySlot.GetComponentInChildren<InventoryItem>();
					if (itemInSlot.itemName == item.itemName)
					{
						//add item to inventory, if current stack > max stack, currentStack - maxStack == newItemStackCount
						//spawn new item stack in inventory with newItemStackCount if inventroy has space
					}
					else
					{
						SpawnNewItemInInventory(inventroySlot, item);
						return;
					}
				}
				else
				{
					SpawnNewItemInInventory(inventroySlot, item);
					return;
				}
			}
		}

		else
		{
			foreach (GameObject inventroySlot in PlayerInventoryUi.Instance.InventorySlots)
			{
				if (inventroySlot.GetComponentInChildren<InventoryItem>() == null)
				{
					SpawnNewItemInInventory(inventroySlot, item);
					return;
				}
			}
		}
	}
	public void TryStackItem(Items item)
	{
		foreach (GameObject inventroySlot in PlayerInventoryUi.Instance.InventorySlots)
		{
			Debug.LogWarning("try stack item");

			InventoryItem itemInSlot;
			if (inventroySlot.GetComponentInChildren<InventoryItem>() != null)
			{
				itemInSlot = inventroySlot.GetComponentInChildren<InventoryItem>();
				if (item.itemName == itemInSlot.itemName) //find correct type
				{
					while (itemInSlot.currentStackCount < itemInSlot.maxStackCount && item.currentStackCount != 0)
					{
						Debug.LogWarning("item in slot count: " + itemInSlot.currentStackCount);
						Debug.LogWarning("new item count: " + item.currentStackCount);
						itemInSlot.currentStackCount++;
						item.currentStackCount--;

						itemInSlot.UpdateStackCounter();
					}
					if (item.currentStackCount >= 1)
					{
						SpawnNewItemInInventory(inventroySlot, item);
						return;
					}
					else return;
				}
				else
				{
					SpawnNewItemInInventory(inventroySlot, item);
					return;
				}
			}
		}
	}
	public void SpawnNewItemInInventory(GameObject inventorySlot, Items item)
	{
		GameObject go = Instantiate(PlayerInventoryUi.Instance.ItemUiPrefab, inventorySlot.transform);
		InventoryItem newitemInSlot = go.GetComponent<InventoryItem>();

		if (item.weaponBaseRef != null)
			AddWeaponToInventory(newitemInSlot, item);

		if (item.armorBaseRef != null)
			AddArmorToInventory(newitemInSlot, item);

		if (item.consumableBaseRef != null)
			AddConsumableToInventory(newitemInSlot, item);

		newitemInSlot.UpdateName();
		newitemInSlot.UpdateImage();
		newitemInSlot.UpdateStackCounter();
	}
	public void AddWeaponToInventory(InventoryItem inventoryItem, Items item)
	{
		inventoryItem.itemName = item.itemName;
		inventoryItem.itemImage = item.itemImage;
		inventoryItem.itemType = (InventoryItem.ItemType)item.weaponBaseRef.itemType;

		inventoryItem.itemLevel = item.itemLevel;
		inventoryItem.rarity = (InventoryItem.Rarity)item.rarity;

		inventoryItem.weaponBaseRef = item.weaponBaseRef;
		inventoryItem.damage = (int)(item.weaponBaseRef.baseDamage * item.statModifier);
		inventoryItem.bonusMana = (int)(item.weaponBaseRef.baseBonusMana * item.statModifier);

		inventoryItem.maxStackCount = item.weaponBaseRef.MaxStackCount;
		inventoryItem.currentStackCount = item.currentStackCount;
	}
	public void AddArmorToInventory(InventoryItem inventoryItem, Items item)
	{
		inventoryItem.itemName = item.itemName;
		inventoryItem.itemImage = item.itemImage;
		inventoryItem.itemType = (InventoryItem.ItemType)item.armorBaseRef.itemType;

		inventoryItem.itemLevel = item.itemLevel;
		inventoryItem.rarity = (InventoryItem.Rarity)item.rarity;

		inventoryItem.armorBaseRef = item.armorBaseRef;
		inventoryItem.baseBonusHealth = (int)(item.armorBaseRef.baseBonusHealth * item.statModifier);
		inventoryItem.bonusMana = (int)(item.armorBaseRef.baseBonusMana * item.statModifier);

		inventoryItem.armorSlot = (InventoryItem.ArmorSlot)item.armorBaseRef.armorSlot;
		inventoryItem.bonusPhysicalResistance = (int)(item.armorBaseRef.bonusPhysicalResistance * item.statModifier);
		inventoryItem.bonusPoisonResistance = (int)(item.armorBaseRef.bonusPoisonResistance * item.statModifier);
		inventoryItem.bonusFireResistance = (int)(item.armorBaseRef.bonusFireResistance * item.statModifier);
		inventoryItem.bonusIceResistance = (int)(item.armorBaseRef.bonusIceResistance * item.statModifier);

		inventoryItem.isStackable = item.isStackable;
		inventoryItem.maxStackCount = item.armorBaseRef.MaxStackCount;
		inventoryItem.currentStackCount = item.currentStackCount;
	}
	public void AddConsumableToInventory(InventoryItem inventoryItem, Items item)
	{
		inventoryItem.itemName = item.itemName;
		inventoryItem.itemImage = item.itemImage;

		inventoryItem.healthRestoration = item.consumableBaseRef.healthRestoration;
		inventoryItem.manaRestoration = item.consumableBaseRef.manaRestoration;

		inventoryItem.isStackable = item.isStackable;
		inventoryItem.maxStackCount = item.consumableBaseRef.MaxStackCount;
		inventoryItem.currentStackCount = item.currentStackCount;
	}
	public void OpenCloseInventory()
	{

	}
}
