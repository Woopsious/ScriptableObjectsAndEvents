using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		if (!IsSlotEmpty())
		{

		}

		GameObject droppeditem = eventData.pointerDrag;
		InventoryItem item = droppeditem.GetComponent<InventoryItem>();
		item.parentAfterDrag = transform;
	}

	public bool IsSlotEmpty()
	{
		if (GetComponentInChildren<InventorySlot>() == null)
			return true;
		else return false;
	}

	public void TryStackItem(Items item)
	{

	}
}
