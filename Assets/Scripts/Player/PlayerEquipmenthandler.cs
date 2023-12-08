using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipmentHandler : EntityEquipmentHandler
{
	public override void Start()
	{
		entityHealth = GetComponent<EntityHealth>();
		//when loading/saving game, once inventory is loaded then load/instantiate equipped items based on loaded inventory
	}
	private void OnEnable()
	{
		InventorySlot.onItemEquip += EquipItem;
	}

	private void OnDisable()
	{
		InventorySlot.onItemEquip -= EquipItem;
		Debug.Log("Player Equipment Handler Disables and Unsubbed");
	}

	public void OnDestroy()
	{
		
	}

	public virtual void EquipItem(InventoryItem item)
	{
		if (item.itemType == InventoryItem.ItemType.isWeapon)
		{
			EquipWeapon(item);
			equippedWeapon = weaponSlotContainer.GetComponentInChildren<Weapons>();// add this for armor pieces
		}

		if (item.armorSlot == InventoryItem.ArmorSlot.helmet)
		{
			EquipArmor(item, equippedHelmet, helmetSlotContainer);
			equippedHelmet = helmetSlotContainer.GetComponentInChildren<Armors>();
		}

		if (item.armorSlot == InventoryItem.ArmorSlot.chestpiece)
		{
			EquipArmor(item, equippedChestpiece, chestpieceSlotContainer);
			equippedChestpiece = chestpieceSlotContainer.GetComponentInChildren<Armors>();
		}

		if (item.armorSlot == InventoryItem.ArmorSlot.legs)
		{
			EquipArmor(item, equippedLegs, legsSlotContainer);
			equippedLegs = legsSlotContainer.GetComponentInChildren<Armors>();
		}

		/* ROBE NOT FULLY IMPLEMENTED
		if (item.armorSlot == InventoryItem.ArmorSlot.robe)
			EquipArmor(item, equippedHelmet, helmetSlotContainer);
		*/
	}

	public void EquipWeapon(InventoryItem weapon)
	{
		GameObject go;

		if (weaponSlotContainer.transform.childCount == 0)
		{
			go = Instantiate(itemPrefab, weaponSlotContainer.transform);
			go.transform.position = Vector3.zero;
			go.AddComponent<Weapons>();
			equippedWeapon = go.GetComponent<Weapons>();
		}

		OnWeaponUnequip();
		equippedWeapon.entityEquipmentHandler = this;

		equippedWeapon.itemName = weapon.itemName;
		equippedWeapon.itemImage = weapon.itemImage;
		equippedWeapon.itemLevel = weapon.itemLevel;
		equippedWeapon.rarity = (Items.Rarity)weapon.rarity;

		equippedWeapon.weaponBaseRef = weapon.weaponBaseRef;
		equippedWeapon.damage = weapon.damage;
		equippedWeapon.bonusMana = weapon.bonusWeaponMana;

		OnWeaponEquip();
	}
	public void EquipArmor(InventoryItem armorToEquip, Armors equippedArmorRef, GameObject slot)
	{
		GameObject go;

		if (slot.transform.childCount == 0)
		{
			go = Instantiate(itemPrefab, slot.transform);
			go.transform.position = Vector3.zero;
			go.AddComponent<Armors>();
			equippedArmorRef = go.GetComponent<Armors>();
		}

		OnArmorUnequip(equippedArmorRef);

		equippedArmorRef.itemName = armorToEquip.itemName;
		equippedArmorRef.itemImage = armorToEquip.itemImage;
		equippedArmorRef.itemLevel = armorToEquip.itemLevel;
		equippedArmorRef.rarity = (Items.Rarity)armorToEquip.rarity;

		equippedArmorRef.armorBaseRef = armorToEquip.armorBaseRef;
		equippedArmorRef.bonusHealth = armorToEquip.bonusArmorHealth;
		equippedArmorRef.bonusMana = armorToEquip.bonusArmorMana;
		equippedArmorRef.bonusPhysicalResistance = armorToEquip.bonusPhysicalResistance;
		equippedArmorRef.bonusPoisonResistance = armorToEquip.bonusPoisonResistance;
		equippedArmorRef.bonusFireResistance = armorToEquip.bonusFireResistance;
		equippedArmorRef.bonusIceResistance = armorToEquip.bonusIceResistance;

		OnArmorEquip(equippedArmorRef);
	}
}
