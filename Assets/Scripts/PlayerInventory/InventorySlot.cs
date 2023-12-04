using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		GameObject droppeditem = eventData.pointerDrag;
		InventoryItem item = droppeditem.GetComponent<InventoryItem>();

		if (IsSlotNotEmpty())
		{
			InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
			itemInSlot.transform.SetParent(item.parentAfterDrag, false);
		}

		item.parentAfterDrag = transform;
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
	public bool IsItemInSlotSame(Items newItem)
	{
		InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
		if (itemInSlot.itemName == newItem.itemName)
			return true;
		else return false;
	}

	public void StackItemOnDrop()
	{

	}
}
