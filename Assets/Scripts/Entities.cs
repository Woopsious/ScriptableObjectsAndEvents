using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Entities : MonoBehaviour
{
	public EntityType entityType;
	public enum EntityType
	{
		isPlayer, isZombie, isSkeleton, isOgre
	}
	public EntityStatsSO stats;

	[Header("health")]
	public int maxHealth;
	public int currentHealth;

	[Header("Resistances")]
	public int physicalDamageResistance;
	public int poisonDamageResistance;
	public int fireDamageResistance;
	public int iceDamageResistance;

	[Header("Speed")]
	public int moveSpeed;
	public int turnSpeed;

	[Header("Mana")]
	public int maxMana;
	public int currentMana;

	public WeaponsSO weapon;

	public int currentWeaponDamage;

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
		currentWeaponDamage = weapon.newDamage;
	}

	public virtual void Init()
	{
		maxHealth = stats.maxHealth;
		currentHealth = stats.maxHealth;
		physicalDamageResistance = stats.physicalDamageResistance;
		poisonDamageResistance = stats.poisonDamageResistance;
		fireDamageResistance = stats.fireDamageResistance;
		iceDamageResistance = stats.iceDamageResistance;
		moveSpeed = stats.moveSpeed;
		turnSpeed = stats.turnSpeed;
		maxMana = stats.maxMana;
		currentMana = stats.maxMana;
	}
	public virtual void RecieveHealing(int health)
	{
		currentHealth += health;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}
	public virtual int CalculateDamageResistance(int damage, DamageType damageType)
	{
		if (damageType == DamageType.isPoisonDamageType)
		{
			Debug.Log("Poison Dmg res: " + poisonDamageResistance);
			return damage -= poisonDamageResistance;
		}
		if (damageType == DamageType.isFireDamageType)
		{
			Debug.Log("Fire Dmg res: " + fireDamageResistance);
			return damage -= fireDamageResistance;
		}
		if (damageType == DamageType.isIceDamageType)
		{
			Debug.Log("Ice Dmg res: " + iceDamageResistance);
			return damage -= iceDamageResistance;
		}
		else
		{
			Debug.Log("Physical Dmg res: " + physicalDamageResistance);
			return damage -= physicalDamageResistance;
		}
	}
	public virtual void RecieveDamage(int damage, DamageType damageType)
	{
		Debug.Log("Obj name: " + stats.entityName + " | Damage recieved: " + damage);

		int healthLost = CalculateDamageResistance(damage, damageType);
		if (healthLost < 2) //always deal 2 damage
			healthLost = 2;

		Debug.Log("health lost after resistance: " + healthLost);
		currentHealth -= healthLost;
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<EnemyController>() != null)
		{
			Debug.Log("Collision");
			other.GetComponent<EnemyController>().RecieveDamage(weapon.newDamage, (DamageType)weapon.damageType);
		}
	}
}
