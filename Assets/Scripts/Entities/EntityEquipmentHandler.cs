using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Pool;

public class EntityEquipmentHandler : MonoBehaviour
{
	public GameObject itemPrefab;

	[Header("Weapon")]
	public List<SOWeapons> possibleWeaponsList = new List<SOWeapons>();
	public GameObject weaponSlotContainer;
	public Weapons equippedWeapon;

	[Header("Armor")]
	public List<SOArmors> possibleHelmetsList = new List<SOArmors>();
	public List<SOArmors> possibleChestpiecesList = new List<SOArmors>();
	public List<SOArmors> possibleLegsList = new List<SOArmors>();

	public GameObject helmetSlotContainer;
	public GameObject chestpieceSlotContainer;
	public GameObject legsSlotContainer;

	public Armors equippedHelmet;
	public Armors equippedChestpiece;
	public Armors equippedLegs;

	[Header("Bonuses Provided By Equipment")]
	public int bonusEquipmentHealth;
	public int bonusEquipmentMana;

	public int bonusEquipmentPhysicalResistance;
	public int bonusEquipmentPoisonResistance;
	public int bonusEquipmentFireResistance;
	public int bonusEquipmentIceResistance;

	/// <summary>
	/// have equipped armor slots here as well that differ slightly from weapons as armor will just add to resistances
	/// where weapons will actually exist in the game world for my planned 2D Dungeon crawler
	/// public List<Armor> possibleHelmetsList = new List<Armor>();
	/// public Armor equippedHelmet
	/// public List<Armor> possibleChestPiecesList = new List<Armor>();
	/// public Armor equippedChestPiece
	/// public List<Armor> possibleLegPiecesList = new List<Armor>();
	/// public Armor equippedLegPiece
	/// </summary>
	/// 

	public void Start()
	{
		EquipWeapon();
		EquipArmor();
	}

	//weapon
	public void EquipWeapon()
	{
		GameObject go;
		int index;

		if (weaponSlotContainer.transform.childCount == 0)
			go = Instantiate(itemPrefab, weaponSlotContainer.transform);
		else go = equippedWeapon.gameObject;

		index = GetRandomNumber(possibleWeaponsList.Count);
		SetUpWeapon(go, possibleWeaponsList, index);
		SetUpWeaponItem(go, possibleWeaponsList, index);
	}
	public void SetUpWeapon(GameObject go, List<SOWeapons> weaponList, int index)
	{
		Weapons weapon;

		if (go.GetComponent<Weapons>() == null)
			weapon = go.AddComponent<Weapons>();
		else weapon = go.GetComponent<Weapons>();

		weapon.itemLevel = GetComponent<EntityHealth>().entityLevel;
		weapon.rarity = Items.Rarity.isCommon;
		weapon.isEquippedByNonPlayer = true;
		weapon.weaponBaseRef = weaponList[index];
		weapon.currentStackCount = 1;

		weapon.GetStatModifier(weapon.itemLevel, (IGetStatModifier.Rarity)weapon.rarity);
		weapon.gameObject.transform.localPosition = Vector3.zero;
	}
	public void SetUpWeaponItem(GameObject go, List<SOWeapons> weaponList, int index)
	{
		Items item = go.GetComponent<Items>();
		item.gameObject.name = weaponList[index].name;
		item.itemName = weaponList[index].name;
		item.itemImage = weaponList[index].itemImage;
		item.ItemPrice = weaponList[index].ItemPrice;
	}

	//armors
	public void EquipArmor()
	{
		GameObject go;
		int index;

		if (helmetSlotContainer.transform.childCount == 0)
			go = Instantiate(itemPrefab, helmetSlotContainer.transform);
		else go = helmetSlotContainer;

		index = GetRandomNumber(possibleHelmetsList.Count);
		SetUpArmor(go, possibleHelmetsList, index);
		SetUpArmorItem(go, possibleHelmetsList, index);

		if (chestpieceSlotContainer.transform.childCount == 0)
			go = Instantiate(itemPrefab, chestpieceSlotContainer.transform);
		else go = chestpieceSlotContainer;

		index = GetRandomNumber(possibleChestpiecesList.Count);
		SetUpArmor(go, possibleChestpiecesList, index);
		SetUpArmorItem(go, possibleChestpiecesList, index);

		if (legsSlotContainer.transform.childCount == 0)
			go = Instantiate(itemPrefab, legsSlotContainer.transform);
		else go = legsSlotContainer;

		index = GetRandomNumber(possibleLegsList.Count);
		SetUpArmor(go, possibleLegsList, index);
		SetUpArmorItem(go, possibleLegsList, index);
	}
	public void SetUpArmor(GameObject go, List<SOArmors> armorList, int index)
	{
		Armors armor;

		if (go.GetComponent<Armors>() == null)
			armor = go.AddComponent<Armors>();
		else armor = go.GetComponent<Armors>();

		armor.itemLevel = GetComponent<EntityHealth>().entityLevel;
		armor.rarity = Items.Rarity.isCommon;
		armor.armorBaseRef = armorList[index];
		armor.currentStackCount = 1;

		armor.GetStatModifier(armor.itemLevel, (IGetStatModifier.Rarity)armor.rarity);
		armor.gameObject.transform.localPosition = Vector3.zero;
	}
	public void SetUpArmorItem(GameObject go, List<SOArmors> armorList, int index)
	{
		Items item = go.GetComponent<Items>();
		item.gameObject.name = armorList[index].name;
		item.itemName = armorList[index].name;
		item.itemImage = armorList[index].itemImage;
		item.ItemPrice = armorList[index].ItemPrice;
	}
	public int GetRandomNumber(int num)
	{
		return UnityEngine.Random.Range(0, num);
	}
}
