using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class PlayerInventoryManager : MonoBehaviour
{
	public static PlayerInventoryManager Instance;

	[Serializable]
	public class OnNewItemEquipEvent : UnityEvent<InventoryItem> { }
	public OnNewItemEquipEvent onNewItemEquipEvent;

	public void Start()
	{
		Instance = this;
		SetUpInventory();
	}
	public void SetUpInventory()
	{
		foreach (GameObject slot in PlayerInventoryUi.Instance.InventorySlots)
		{
			InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
			inventorySlot.SetUpInventorySlots();

			if (inventorySlot.slotType == InventorySlot.SlotType.generic) return;
		}
	}

	public void AddItemToPlayerInventory(Items item)
	{
		if (item.isStackable)
			TryStackItem(item);
		else
			SpawnNewItemInInventory(item);
	}
	public void TryStackItem(Items newItem)
	{
		for (int i = 0; i < PlayerInventoryUi.Instance.InventorySlots.Count; i++)
		{
			InventorySlot inventroySlot = PlayerInventoryUi.Instance.InventorySlots[i].GetComponent<InventorySlot>();
			InventoryItem itemInSlot;

			if (inventroySlot.IsSlotNotEmpty())
			{
				itemInSlot = inventroySlot.GetComponentInChildren<InventoryItem>();
				if (inventroySlot.IsItemInSlotSameAs(newItem) && itemInSlot.currentStackCount < itemInSlot.maxStackCount)
				{
					//add to itemInSlot.CurrentStackCount till maxStackCountReached
					for (int itemCount = itemInSlot.currentStackCount; itemCount < itemInSlot.maxStackCount; itemCount++)
					{
						if (newItem.currentStackCount <= 0) return; //stop adding when newItem.currentStackCount == 0

						newItem.currentStackCount--;
						itemInSlot.currentStackCount++;
						itemInSlot.UpdateStackCounter();
					}
					if (newItem.currentStackCount != 0) //if stackcount still has more find next stackable item
						continue;
				}
				else
					continue;
			}
			else if (newItem.currentStackCount > 0)
			{
				SpawnNewItemInInventory(newItem);
				return;
			}
		}
	}
	public void SpawnNewItemInInventory(Items item)
	{
		for (int i = 0; i < PlayerInventoryUi.Instance.InventorySlots.Count; i++)
		{
			InventorySlot inventorySlot = PlayerInventoryUi.Instance.InventorySlots[i].GetComponent<InventorySlot>();

			if (!inventorySlot.IsSlotNotEmpty())
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
				newitemInSlot.inventorySlotIndex = i;
				return;
			}
		}
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
		inventoryItem.consumableBaseRef = item.consumableBaseRef;

		inventoryItem.healthRestoration = item.consumableBaseRef.healthRestoration;
		inventoryItem.manaRestoration = item.consumableBaseRef.manaRestoration;

		inventoryItem.isStackable = item.isStackable;
		inventoryItem.maxStackCount = item.consumableBaseRef.MaxStackCount;
		inventoryItem.currentStackCount = item.currentStackCount;
	}

	public bool CheckIfInventoryFull()
	{
		int numOfFilledSlots = 0;

		foreach (GameObject obj in PlayerInventoryUi.Instance.InventorySlots)
		{
			InventorySlot inventorySlot = obj.GetComponent<InventorySlot>();

			if (inventorySlot.IsSlotNotEmpty())
				numOfFilledSlots++;
		}
		Debug.LogWarning("number of inventroy slots currently full: " + numOfFilledSlots);
		Debug.LogWarning("max number of inventroy slots: " + PlayerInventoryUi.Instance.InventorySlots.Count);

		if (numOfFilledSlots == PlayerInventoryUi.Instance.InventorySlots.Count)
			return true;
		else return false;
	}
}
