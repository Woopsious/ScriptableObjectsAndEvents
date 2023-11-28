using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;

	public List<InventoryItem> playerInventory = new List<InventoryItem>();

	public void Start()
	{
		Instance = this;
	}
	public void Update()
	{

	}

	public void AddItemToPlayerInventory(Items item)
	{
		if (item.weaponBaseRef != null)
			AddWeaponToInventory(item);

		if (item.armorBaseRef != null)
			AddArmorToInventory(item);
	}
	public void AddWeaponToInventory(Items item)
	{
		InventoryItem itemData = new()
		{
			itemName = item.itemName,
			itemImage = item.itemImage,
			itemType = (InventoryItem.ItemType)item.weaponBaseRef.itemType,

			itemLevel = item.itemLevel,
			rarity = (InventoryItem.Rarity)item.rarity,

			weaponBaseRef = item.weaponBaseRef,
			damage = (int)(item.weaponBaseRef.baseDamage * item.statModifier),
			bonusMana = (int)(item.weaponBaseRef.baseBonusMana * item.statModifier),

			maxStackCount = item.weaponBaseRef.MaxStackCount,
			currentStackCount = item.currentStackCount
		};
		playerInventory.Add(itemData);
	}
	public void AddArmorToInventory(Items item)
	{
		InventoryItem itemData = new()
		{
			itemName = item.itemName,
			itemImage = item.itemImage,
			itemType = (InventoryItem.ItemType)item.armorBaseRef.itemType,

			itemLevel = item.itemLevel,
			rarity = (InventoryItem.Rarity)item.rarity,

			armorBaseRef = item.armorBaseRef,
			baseBonusHealth = (int)(item.armorBaseRef.baseBonusHealth * item.statModifier),
			bonusMana = (int)(item.armorBaseRef.baseBonusMana * item.statModifier),

			armorSlot = (InventoryItem.ArmorSlot)item.armorBaseRef.armorSlot,
			bonusPhysicalResistance = (int)(item.armorBaseRef.bonusPhysicalResistance * item.statModifier),
			bonusPoisonResistance = (int)(item.armorBaseRef.bonusPoisonResistance * item.statModifier),
			bonusFireResistance = (int)(item.armorBaseRef.bonusFireResistance * item.statModifier),
			bonusIceResistance = (int)(item.armorBaseRef.bonusIceResistance * item.statModifier),

			maxStackCount = item.armorBaseRef.MaxStackCount,
			currentStackCount = item.currentStackCount
		};
		playerInventory.Add(itemData);
	}
}
