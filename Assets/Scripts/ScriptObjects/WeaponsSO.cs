using UnityEngine;
using static Weapons;

[CreateAssetMenu(fileName = "WeaponsScriptableObject", menuName = "Items/Weapons")]
public class WeaponsSO : ItemsSO
{
	[Header("Weapon Info")]
	public bool isBareHands;
	public int damage;
	public float attackSpeed;

	[Header("Rarity Type")]
	public Rarity rarity;
	public enum Rarity
	{
		isCommon, isRare, isLegendary
	}

	[Header("Damage Type")]
	public DamageType damageType;
	public enum DamageType
	{
		isPhysicalDamageType, isPoisonDamageType, isFireDamageType, isIceDamageType
	}

	[Header("Ranged Weapon Toggles")]
	public bool isRangedWeapon;
	public float attackRange;
	public float maxAttackRange;

	[Header("Magic Weapon Toggles")]
	public int mana;
	public int manaCost;

	public int newDamage;

	public void OnEnable()
	{
		GetModifier(rarity);
	}

	public void GetModifier(Rarity rarity)
	{
		float modifier = 1.25f;
		if (rarity == Rarity.isLegendary) { modifier += 0.25f; }
		if (rarity == Rarity.isRare) { modifier += 0.1f; }
		else { modifier += 0; }

		//modifier += (float)WeaponItemLevel / 10;

		Debug.Log(modifier);
		//Debug.Log(WeaponItemLevel / 10);

		newDamage = (int)(damage * modifier);
	}
}
