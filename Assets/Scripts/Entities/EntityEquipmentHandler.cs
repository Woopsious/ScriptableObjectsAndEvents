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

	[HideInInspector] public EntityHealth entityHealth;

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

	public virtual void Start()
	{
		entityHealth = GetComponent<EntityHealth>();
		EquipWeapon(null);
		EquipArmor(null);
	}

	//weapon
	public virtual void EquipWeapon(InventoryItem weapon)
	{
		GameObject go;
		int index;

		if (weaponSlotContainer.transform.childCount == 0)
		{
			go = Instantiate(itemPrefab, weaponSlotContainer.transform);
			go.AddComponent<Weapons>();
			equippedWeapon = go.GetComponent<Weapons>();
		}

		index = Utilities.GetRandomNumber(possibleWeaponsList.Count);
		if (equippedWeapon != null)
		{
			equippedWeapon.weaponBaseRef = possibleWeaponsList[index];
			equippedWeapon.entityEquipmentHandler = this;
			equippedWeapon.SetItemStats(Items.Rarity.isCommon, GetComponent<EntityHealth>().entityLevel);
		}
	}
	public void OnWeaponUnequip()
	{
		if (equippedWeapon != null)
		{
			bonusEquipmentMana -= equippedWeapon.bonusMana;

			entityHealth.totalMaxMana = entityHealth.maxMana + bonusEquipmentMana;
			entityHealth.totalCurrentMana = entityHealth.currentMana + bonusEquipmentMana;
		}
	}
	public void OnWeaponEquip()
	{
		bonusEquipmentMana += equippedWeapon.bonusMana;

		entityHealth.totalMaxMana = entityHealth.maxMana + bonusEquipmentMana;
		entityHealth.totalCurrentMana = entityHealth.currentMana + bonusEquipmentMana;
	}

	//armors
	public virtual void EquipArmor(InventoryItem armor)
	{
		GameObject go;
		int index;

		if (possibleHelmetsList.Count != 0)
		{
			if (helmetSlotContainer.transform.childCount == 0)
			{
				go = Instantiate(itemPrefab, helmetSlotContainer.transform);
				go.transform.position = Vector3.zero;
				go.AddComponent<Armors>();
				equippedHelmet = go.GetComponent<Armors>();
			}

			index = Utilities.GetRandomNumber(possibleHelmetsList.Count);
			if (equippedHelmet != null)
			{
				equippedHelmet.armorBaseRef = possibleHelmetsList[index];
				equippedHelmet.entityEquipmentHandler = this;
				equippedHelmet.SetItemStats(Items.Rarity.isCommon, GetComponent<EntityHealth>().entityLevel);
			}
		}

		if (possibleChestpiecesList.Count != 0)
		{
			if (chestpieceSlotContainer.transform.childCount == 0)
			{
				go = Instantiate(itemPrefab, chestpieceSlotContainer.transform);
				go.transform.position = Vector3.zero;
				go.AddComponent<Armors>();
				equippedChestpiece = go.GetComponent<Armors>();
			}

			index = Utilities.GetRandomNumber(possibleChestpiecesList.Count);
			if (equippedChestpiece != null)
			{
				equippedChestpiece.armorBaseRef = possibleChestpiecesList[index];
				equippedChestpiece.entityEquipmentHandler = this;
				equippedChestpiece.SetItemStats(Items.Rarity.isCommon, GetComponent<EntityHealth>().entityLevel);
			}
		}

		if (possibleLegsList.Count != 0)
		{
			if (legsSlotContainer.transform.childCount == 0)
			{
				go = Instantiate(itemPrefab, legsSlotContainer.transform);
				go.transform.position = Vector3.zero;
				go.AddComponent<Armors>();
				equippedLegs = go.GetComponent<Armors>();
			}

			index = Utilities.GetRandomNumber(possibleLegsList.Count);
			if (equippedLegs != null)
			{
				equippedLegs.armorBaseRef = possibleLegsList[index];
				equippedLegs.entityEquipmentHandler = this;
				equippedLegs.SetItemStats(Items.Rarity.isCommon, GetComponent<EntityHealth>().entityLevel);
			}
		}
	}
	public void OnArmorUnequip(Armors armor)
	{
		if (armor != null)
		{
			bonusEquipmentHealth -= armor.bonusHealth;
			bonusEquipmentMana -= armor.bonusMana;
			bonusEquipmentPhysicalResistance -= armor.bonusPhysicalResistance;
			bonusEquipmentPoisonResistance -= armor.bonusPoisonResistance;
			bonusEquipmentFireResistance -= armor.bonusFireResistance;
			bonusEquipmentIceResistance -= armor.bonusIceResistance;

			entityHealth.totalMaxHealth = entityHealth.maxHealth - bonusEquipmentHealth;
			entityHealth.totalCurrentHealth = entityHealth.currentHealth - bonusEquipmentHealth;
			entityHealth.totalMaxMana = entityHealth.maxMana - bonusEquipmentMana;
			entityHealth.totalCurrentMana = entityHealth.currentMana - bonusEquipmentMana;

			entityHealth.totalPhyicalResistance = entityHealth.physicalResistance - bonusEquipmentPhysicalResistance;
			entityHealth.totalPoisonResistance = entityHealth.poisonResistance - bonusEquipmentPoisonResistance;
			entityHealth.totalFireResistance = entityHealth.fireResistance - bonusEquipmentFireResistance;
			entityHealth.totalIceResistance = entityHealth.iceResistance - bonusEquipmentIceResistance;
		}
	}
	public void OnArmorEquip(Armors armor)
	{
		bonusEquipmentHealth += armor.bonusHealth;
		bonusEquipmentMana += armor.bonusMana;
		bonusEquipmentPhysicalResistance += armor.bonusPhysicalResistance;
		bonusEquipmentPoisonResistance += armor.bonusPoisonResistance;
		bonusEquipmentFireResistance += armor.bonusFireResistance;
		bonusEquipmentIceResistance += armor.bonusIceResistance;

		entityHealth.totalMaxHealth = entityHealth.maxHealth + bonusEquipmentHealth;
		entityHealth.totalCurrentHealth = entityHealth.currentHealth + bonusEquipmentHealth;
		entityHealth.totalMaxMana = entityHealth.maxMana + bonusEquipmentMana;
		entityHealth.totalCurrentMana = entityHealth.currentMana + bonusEquipmentMana;

		entityHealth.totalPhyicalResistance = entityHealth.physicalResistance + bonusEquipmentPhysicalResistance;
		entityHealth.totalPoisonResistance = entityHealth.poisonResistance + bonusEquipmentPoisonResistance;
		entityHealth.totalFireResistance = entityHealth.fireResistance + bonusEquipmentFireResistance;
		entityHealth.totalIceResistance = entityHealth.iceResistance + bonusEquipmentIceResistance;
	}
}
