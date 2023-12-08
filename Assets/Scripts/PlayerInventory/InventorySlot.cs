using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	[Serializable]
	public class OnNewItemEquipEvent : UnityEvent<InventoryItem> { }
	public OnNewItemEquipEvent onNewItemEquipEvent;

	public SlotType slotType;
	public enum SlotType
	{
		generic, weapon, helmet, chestpiece, legs, robe, consumables
	}

	public int slotIndex;

	public void SetUpInventorySlots()
	{
		slotIndex = transform.GetSiblingIndex();
	}
	public void SetUpEquipItemEvents()
	{
		if (slotType == SlotType.generic) return;
		else
		{
			PlayerEquipmentHandler equipmentHandler = PlayerInventoryManager.Instance.GetComponent<PlayerEquipmentHandler>();
			onNewItemEquipEvent.AddListener(delegate { equipmentHandler.EquipItem(gameObject.GetComponentInChildren<InventoryItem>()); }) ;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		InventoryItem itemInSlot;
		GameObject droppeditem = eventData.pointerDrag;
		InventoryItem item = droppeditem.GetComponent<InventoryItem>();

		if (!IsCorrectSlotType(item)) return;

		if (IsSlotNotEmpty())
		{
			itemInSlot = GetComponentInChildren<InventoryItem>();
			itemInSlot.transform.SetParent(item.parentAfterDrag, false);
			itemInSlot.inventorySlotIndex = item.inventorySlotIndex;
		}

		item.parentAfterDrag = transform;
		item.inventorySlotIndex = slotIndex;

		if (slotType == SlotType.generic) return;

		onNewItemEquipEvent.Invoke(item);
	}

	public InventoryItem ReturnItemInSlot(InventoryItem item)
	{
		return item;
	}

	public bool IsSlotNotEmpty()
	{
		if (GetComponentInChildren<InventoryItem>() == null)
			return false;
		else return true;
	}
	public bool IsItemInSlotStackable()
	{
		InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
		if (itemInSlot.isStackable)
			return true;
		else return false;
	}
	public bool IsItemInSlotSameAs(Items newItem)
	{
		InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
		if (itemInSlot.itemName == newItem.itemName)
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
