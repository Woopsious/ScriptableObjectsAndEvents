using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public PlayerEquipmentHandler playerEquipmentHandler;

	public static event Action<InventoryItem> onItemEquip;

	public SlotType slotType;
	public enum SlotType
	{
		generic, weapon, helmet, chestpiece, legs, robe, consumables
	}

	public int slotIndex;
	public InventoryItem itemInSlot;

	public void Start()
	{
		slotIndex = transform.GetSiblingIndex();
	}

	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppeditem = eventData.pointerDrag;
		InventoryItem item = droppeditem.GetComponent<InventoryItem>();

		if (!IsCorrectSlotType(item)) return;

		if (!IsSlotEmpty()) //swap slot data
		{
			if (IsItemInSlotStackable())
			{

			}

			itemInSlot.transform.SetParent(item.parentAfterDrag, false);
			itemInSlot.inventorySlotIndex = item.inventorySlotIndex;
			item.parentAfterDrag.GetComponent<InventorySlot>().itemInSlot = itemInSlot;
		}
		else //set ref to null
		{
			item.parentAfterDrag.GetComponent<InventorySlot>().itemInSlot = null;
		}

		//set new slot data
		item.parentAfterDrag = transform;
		item.inventorySlotIndex = slotIndex;
		itemInSlot = item;

		if (slotType == SlotType.generic) return;
		onItemEquip?.Invoke(item);
	}

	public bool IsSlotEmpty()
	{
		if (GetComponentInChildren<InventoryItem>() == null)
			return true;
		else return false;
	}
	public bool IsItemInSlotStackable()
	{
		InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
		if (itemInSlot.isStackable)
			return true;
		else return false;
	}
	public bool IsItemInSlotSameAs(InventoryItem Item)
	{
		InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
		if (itemInSlot.itemName == Item.itemName)
			return true;
		else return false;
	}
	public bool IsCorrectSlotType(InventoryItem item)
	{
		if (slotType == SlotType.generic)
			return true;
		if (item.itemType == InventoryItem.ItemType.isConsumable && slotType == SlotType.consumables)
			return true;
		else if (item.itemType == InventoryItem.ItemType.isWeapon && slotType == SlotType.weapon)
			return true;
		else if (item.itemType == InventoryItem.ItemType.isArmor)
		{
			if (item.armorSlot == InventoryItem.ArmorSlot.helmet && slotType == SlotType.helmet)
				return true;
			if (item.armorSlot == InventoryItem.ArmorSlot.chestpiece && slotType == SlotType.chestpiece)
				return true;
			if (item.armorSlot == InventoryItem.ArmorSlot.legs && slotType == SlotType.legs)
				return true;
			if (item.armorSlot == InventoryItem.ArmorSlot.robe && slotType == SlotType.robe)
				return true;
			else return false;
		}
		else return false;
	}
}
