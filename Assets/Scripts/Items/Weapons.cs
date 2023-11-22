using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapons : Items, IGetStatModifier
{
	[Header("Weapon Info")]
	public int damage;
	public int bonusMana;
	public bool isEquipped;

	public override void Start()
	{
		base.Start();
		SetWeaponStats();
	}
	public void SetWeaponStats()
	{
		damage = (int)(weaponBaseRef.baseDamage * statModifier);
		bonusMana = (int)(weaponBaseRef.baseBonusMana * statModifier);
	}
	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<EntityHealth>() != null && isEquipped)
		{
			other.GetComponent<EntityHealth>().RecieveDamage(damage,
				(IDamagable.DamageType)weaponBaseRef.baseDamageType);
		}
	}
}
