using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsScriptableObject", menuName = "Items/Weapons")]
public class WeaponsSO : ItemsSO
{
	[Header("Weapon toggles")]
	public bool isBareHands;
	public int damage;
	public float attackSpeed;

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
}
