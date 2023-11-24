using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
	[Header("Item Info")]
	public string itemName;
	public Image itemImage;
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

	[Header("Item Dynamic Info")]
	public int maxStackCount;
	public int currentStackCount;

	[Header("Weapon Info")]
	public SOWeapons weaponBaseRef;
	public int damage;
	public int bonusMana;

	/// 
	/// armor specific info like all resistances and extra health etc.
	/// 

	[Header("Consumable Base Ref")]
	public SOConsumables consumableBaseRef;
	public int healthRestoration;
	public int manaRestoration;
}
