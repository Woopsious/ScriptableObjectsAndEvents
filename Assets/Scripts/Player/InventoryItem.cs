using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem : MonoBehaviour
{
	public TMP_Text uiName;
	public Image uiImage;
	public TMP_Text uiStackCount;

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
	public bool isStackable;
	public int maxStackCount;
	public int currentStackCount;
	public int inventroySlot;

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

	public void UpdateName()
	{
		uiName.text = itemName;
	}

	public void UpdateImage()
	{
		//no images for 3d version
	}

	public void UpdateStackCounter()
	{
		uiStackCount.text = currentStackCount.ToString();
	}
}
