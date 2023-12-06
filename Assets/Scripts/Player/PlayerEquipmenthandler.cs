using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipmentHandler : EntityEquipmentHandler
{
	public class OnNewItemEquipEvent : UnityEvent<InventoryItem> { }
	public OnNewItemEquipEvent onNewItemEquipEvent;

	public override void Start()
	{
		//when loading/saving game, once inventory is loaded then load/instantiate equipped items based on loaded inventory
	}

	public override void EquipWeapon(InventoryItem weapon)
	{
		GameObject go;

		if (weaponSlotContainer.transform.childCount == 0)
		{
			go = Instantiate(itemPrefab, weaponSlotContainer.transform);
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
		equippedWeapon.bonusMana = weapon.bonusMana;
	}
	public override void EquipArmor(InventoryItem armor)
	{

	}
}
