using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
	public WeaponsSO weaponType;

	[Header("Weapon Info")]
	public int weaponItemLevel;
	public int damage;
	public float attackSpeed;

	[Header("Damage Type")]
	public DamageType damageType;
	public enum DamageType
	{
		isPhysicalDamageType, isPoisonDamageType, isFireDamageType, isIceDamageType
	}
	[Header("Rarity Type")]
	public Rarity rarity;
	public enum Rarity
	{
		isCommon, isRare, isLegendary
	}

	[Header("Ranged Weapon Toggles")]
	public bool isRangedWeapon;
	public float attackRange;
	public float maxAttackRange;

	[Header("Magic Weapon Toggles")]
	public int mana;
	public int manaCost;

	public void Start()
	{
		
	}
	public void Init()
	{

	}
	public void OnEnable()
	{
		GetModifier(rarity);
	}
	public void GetModifier(Rarity rarity)
	{
		float modifier = 1.25f;
		/*
		if (rarity == Rarity.isLegendary) { modifier += 0.25f; }
		if (rarity == Rarity.isRare) { modifier += 0.1f; }
		else { modifier += 0; }

		modifier += (float)WeaponItemLevel / 10;

		Debug.Log(modifier);
		Debug.Log(WeaponItemLevel / 10);

		newDamage = (int)(damage * modifier);
		*/
	}
}
