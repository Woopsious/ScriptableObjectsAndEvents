using UnityEngine;
using static Weapons;

[CreateAssetMenu(fileName = "WeaponsScriptableObject", menuName = "Items/Weapons")]
public class WeaponsSO : ItemsSO
{
	[Header("Weapon Info")]
	public bool isBareHands;
	public int baseDamage;
	public float baseAttackSpeed;
	public float baseKnockback;

	[Header("Damage Type")]
	public DamageType baseDamageType;
	public enum DamageType
	{
		isPhysicalDamageType, isPoisonDamageType, isFireDamageType, isIceDamageType
	}

	[Header("Ranged Weapon Toggles")]
	public bool isRangedWeapon;
	public float baseAttackRange;
	public float baseMaxAttackRange;

	[Header("Magic Weapon Toggles")]
	public int baseBonusMana;
}
