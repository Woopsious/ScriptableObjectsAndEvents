using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour, IGetStatModifier
{
	[Header("Weapon Info")]
	public WeaponsSO weaponType;

	[Header("Changeable")]
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

	public void Start()
	{
		Init();
	}
	public void Init()
	{
		GetStatModifier(itemLevel, (IGetStatModifier.Rarity)rarity);
	}

	public void GetStatModifier(int itemLevel, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		if (rarity == IGetStatModifier.Rarity.isLegendary) { modifier += 0.25f; } //get rarity modifier
		if (rarity == IGetStatModifier.Rarity.isRare) { modifier += 0.1f; }
		else { modifier += 0; }

		modifier += (float)itemLevel / 20 - 0.025f;  //get level modifier

		damage = (int)(weaponType.baseDamage * modifier);
		bonusMana = (int)(weaponType.baseBonusMana * modifier);
	}
	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<EntityHealth>() != null && other.GetComponent<EntityHealth>().isDamagable)
		{
			other.GetComponent<EntityHealth>().RecieveDamage(damage,
				(IDamagable.DamageType)weaponType.baseDamageType);
		}
		else
		{
			Debug.Log(other.name);
		}
	}
}
