using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.PackageManager;
using UnityEngine;

public class DroppedItems : MonoBehaviour
{
	///
	/// items that are dropped on the ground and can be picked up via Interactable Component script
	/// ItemsSO should hold basic item info, for instance using that variable i set in Init 
	///

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
	public WeaponsSO weaponBaseRef;
	public ConsumablesSO consumableBaseRef;

	[Header("Item Dynamic Info")]
	public int currentStackCount;
	public float statModifier;

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
	}
	public void GetStatModifier(int level, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		if (rarity == IGetStatModifier.Rarity.isLegendary) { modifier += 0.25f; } //get rarity modifier
		if (rarity == IGetStatModifier.Rarity.isRare) { modifier += 0.1f; }
		else { modifier += 0; }

		statModifier = modifier + (level - 1f) / 20;  //get level modifier
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

		PlayerInventory.Instance.AddItemToPlayerInventory(this);
		Destroy(gameObject);
	}
}
