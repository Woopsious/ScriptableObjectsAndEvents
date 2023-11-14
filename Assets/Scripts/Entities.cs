using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Entities : MonoBehaviour
{
	public EntityStatsSO stats;
	public WeaponsSO weapon;

	public DamageType damageType;
	public enum DamageType
	{
		isPhysicalDamageType, isPoisonDamageType, isFireDamageType, isIceDamageType
	}

	public void Start()
	{
		Init();
	}
	public virtual void Update()
	{

	}

	public virtual void Init()
	{

	}

	public virtual void RecieveHealing(int health)
	{
		stats.health += health;
		if (stats.health > stats.maxHealth)
			stats.health = stats.maxHealth;
	}
	public virtual void RecieveDamage(int damage, DamageType damageType)
	{
		Debug.Log(gameObject.name);

		if (damageType == DamageType.isPhysicalDamageType)
		{
			Debug.Log("physical Dmg");
			damage -= stats.physicalDamageResistance;
		}
		if (damageType == DamageType.isFireDamageType)
		{
			Debug.Log("fire Dmg");
			damage -= stats.fireDamageResistance;
		}

		Debug.Log(damage);

		if (damage < 5)
			damage = 5;

		stats.health -= damage;
		Debug.Log(stats.health);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Entities>() != null)
		{
			Debug.Log("Collision");
			other.GetComponent<Entities>().RecieveDamage(weapon.damage, (DamageType)weapon.damageType);
		}
	}
}
