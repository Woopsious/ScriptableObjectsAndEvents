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

	public ItemType itemType;
	public enum ItemType
	{
		isConsumable, isWeapon, isArmor
	}
	public int currentStackCount;

	[Header("Consumable Type Info")]
	public ConsumablesSO consumable;

	[Header("Weapon Type Info")]
	public WeaponsSO weapon;
	public int itemLevel;
	public Rarity rarity;
	public enum Rarity
	{
		isCommon, isRare, isLegendary
	}

	[Header("UnChangeable")]
	public int damage;
	public int bonusMana;
	private float modifier;

	public virtual void Start()
	{
		Init();
	}

	public virtual void OnMouseOverItem()
	{
		//display what item it is eg: item price and name and rarity
		//in child classes also display weapon stats if weapon or armor stats if armor
	}

	public virtual void Init()
	{
		//On death of enemy entity set Item variable on this Objs Instantiation, randomly pick from selection of items in enemy loot pool
	}

	public virtual void PickUpItem()
	{
		//called from Interactable Component script that adds item to inventory passing through necessary info
		//in child classes pass through item type specific 

		PlayerInventory.Instance.AddItemToPlayerInventory(this);
	}
}
