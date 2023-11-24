using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItems
{
	[Header("Item Info")]
	public int itemLevel;
	public ItemType itemType;
	public enum ItemType
	{
		isConsumable, isWeapon, isArmor
	}
	public Rarity rarity;
	public enum Rarity
	{
		isCommon, isRare, isLegendary
	}

	[Header("Item Base Ref")]
	public SOWeapons weaponBaseRef;
	public SOConsumables consumableBaseRef;

	[Header("Item Dynamic Info")]
	public int currentStackCount;
	public int maxStackCount;
}
