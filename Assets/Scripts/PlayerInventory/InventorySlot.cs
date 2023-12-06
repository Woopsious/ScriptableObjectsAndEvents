using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
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
	}

	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppeditem = eventData.pointerDrag;
		InventoryItem item = droppeditem.GetComponent<InventoryItem>();

		if (!IsCorrectSlotType(item)) return;

		if (IsSlotNotEmpty())
		{
			InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
			itemInSlot.transform.SetParent(item.parentAfterDrag, false);
			itemInSlot.inventorySlotIndex = item.inventorySlotIndex;
		}

		item.parentAfterDrag = transform;
		item.inventorySlotIndex = slotIndex;
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
		{
			PlayerInventoryManager.Instance.onNewItemEquipEvent.Invoke(item);
			return true;
		}
		else if (item.itemType == InventoryItem.ItemType.isWeapon && slotType == SlotType.weapon)
		{
			PlayerInventoryManager.Instance.onNewItemEquipEvent.Invoke(item);
			return true;
		}
		else if (item.itemType == InventoryItem.ItemType.isArmor)
		{
			PlayerInventoryManager.Instance.onNewItemEquipEvent.Invoke(item);

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
