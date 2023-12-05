using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
	[HideInInspector] public EntityEquipmentHandler entityEquipmentHandler;

	[Header("Item Info")]
	public string itemName;
	public Image itemImage;
	public int ItemPrice;
	public int ItemId;

	public int itemLevel;
	public Rarity rarity;
	public enum Rarity
	{
		isCommon, isRare, isLegendary
	}

	[Header("Item Base Ref")]
	public SOWeapons weaponBaseRef;
	public SOArmors armorBaseRef;
	public SOConsumables consumableBaseRef;

	[Header("Inventroy Dynamic Info")]
	public bool isStackable;
	public int currentStackCount;
	public float statModifier;
	public int inventroySlot;

	public virtual void Start()
	{
		//On death of enemy entity set Item variable on this Objs Instantiation, randomly pick from selection of items in enemy loot pool
		GetStatModifier(itemLevel, (IGetStatModifier.Rarity)rarity);
	}
	public void OnItemDrop(Rarity setRarity, int setLevel)
	{
		rarity = setRarity;
		itemLevel = setLevel;
		GetStatModifier(itemLevel, (IGetStatModifier.Rarity)rarity);

		name = GetItemName();
		itemName = GetItemName();
		itemImage = GetItemImage();
		ItemPrice = GetItemPrice();
	}
	public void GetStatModifier(int level, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		if (rarity == IGetStatModifier.Rarity.isLegendary) { modifier += 0.25f; } //get rarity modifier
		if (rarity == IGetStatModifier.Rarity.isRare) { modifier += 0.1f; }
		else { modifier += 0; }

		statModifier = modifier + (level - 1f) / 20;  //get level modifier
	}
	public string GetItemName()
	{
		if (weaponBaseRef != null) return weaponBaseRef.itemName;
		else if (armorBaseRef != null) return armorBaseRef.itemName;
		else return consumableBaseRef.itemName;
	}
	public Image GetItemImage()
	{
		if (weaponBaseRef != null) return weaponBaseRef.itemImage;
		else if (armorBaseRef != null) return armorBaseRef.itemImage;
		else return consumableBaseRef.itemImage;
	}
	public int GetItemPrice()
	{
		if (weaponBaseRef != null) return (int)(weaponBaseRef.ItemPrice * statModifier);
		else if (armorBaseRef != null) return (int)(armorBaseRef.ItemPrice * statModifier);
		else return (int)(consumableBaseRef.ItemPrice * statModifier);
	}

	public virtual void OnMouseOverItem()
	{
		//display what item it is eg: item price and name and rarity
		//in child classes also display weapon stats if weapon or armor stats if armor
	}

	public virtual void PickUpItem()
	{
		//called from Interactable Component script that adds item to inventory passing through necessary info
		//in child classes pass through item type specific 

		if (PlayerInventory.Instance.CheckIfInventoryFull())
		{
			Debug.LogWarning("Inventory is full");
			return;
		}

		PlayerInventory.Instance.AddItemToPlayerInventory(this);
		Destroy(gameObject);
	}
}
