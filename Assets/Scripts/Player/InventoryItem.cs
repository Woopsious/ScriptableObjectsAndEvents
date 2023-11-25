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

	[Header("Armor Info")]
	public int baseBonusHealth;
	public int baseBonusMana;

	[Header("Armor Slot")]
	public SOArmors armorBaseRef;
	public ArmorSlot armorSlot;
	public enum ArmorSlot
	{
		helmet, chestpiece, legs, robe
	}

	[Header("Armor Resistances")]
	public int bonusPhysicalResistance;
	public int bonusPoisonResistance;
	public int bonusFireResistance;
	public int bonusIceResistance;

	[Header("Consumable Base Ref")]
	public SOConsumables consumableBaseRef;
	public int healthRestoration;
	public int manaRestoration;
}
