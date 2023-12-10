using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapons : Items, IGetStatModifier
{
	[Header("Weapon Info")]
	public int damage;
	public int bonusMana;
	public bool isEquippedByPlayer;
	public bool isEquippedByNonPlayer;

	public void Start()
	{
		if (generateStatsOnStart)
			SetItemStats(rarity, itemLevel);
	}

	public override void SetItemStats(Rarity setRarity, int setLevel)
	{
		base.SetItemStats(setRarity, setLevel);

		damage = (int)(weaponBaseRef.baseDamage * statModifier);
		bonusMana = (int)(weaponBaseRef.baseBonusMana * statModifier);
		isStackable = weaponBaseRef.isStackable;

		if (entityEquipmentHandler != null) //for non player
			entityEquipmentHandler.OnWeaponEquip(this, false, true);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (isEquippedByPlayer && other.GetComponent<EntityHealth>() != null)
		{
			other.GetComponent<EntityHealth>().RecieveDamage(damage, (IDamagable.DamageType)weaponBaseRef.baseDamageType);
		}
		else if (isEquippedByNonPlayer && other.GetComponent<PlayerController>())
		{
			other.GetComponent<EntityHealth>().RecieveDamage(damage, (IDamagable.DamageType)weaponBaseRef.baseDamageType);
		}
	}
}
