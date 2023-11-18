using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Entities : MonoBehaviour, IDamagable
{
	[Header("Entity Info")]
	public EntityBaseStatsSO stats;

	[Header("Health")]
	public int maxHealth;
	public int currentHealth;

	[Header("Resistances")]
	public int physicalDamageResistance;
	public int poisonDamageResistance;
	public int fireDamageResistance;
	public int iceDamageResistance;

	[Header("Speed")]
	public float moveSpeed;
	public float turnSpeed;

	[Header("Mana")]
	public int maxMana;
	public int currentMana;

	[Header("Equipped WeaponInfo")]
	public GameObject equippedWeaponContainer;
	public Weapons equippedWeapon;

	[Header("Entity Body")]
	public NavMeshAgent navMeshAgent;
	public Rigidbody rb;

	public void Start()
	{
		Init();
	}
	public virtual void Update()
	{

	}

	public virtual void Init()
	{
		maxHealth = stats.maxHealth;
		currentHealth = stats.maxHealth;
		physicalDamageResistance = stats.physicalDamageResistance;
		poisonDamageResistance = stats.poisonDamageResistance;
		fireDamageResistance = stats.fireDamageResistance;
		iceDamageResistance = stats.iceDamageResistance;
		moveSpeed = stats.navMeshMoveSpeed;
		turnSpeed = stats.navMeshTurnSpeed;
		maxMana = stats.maxMana;
		currentMana = stats.maxMana;
	}
	public virtual void RecieveHealing(int health)
	{
		currentHealth += health;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}
	public void RecieveDamage(int damage, IDamagable.DamageType damageType)
	{
		Debug.Log("Obj name: " + stats.entityName + " | Damage recieved: " + damage);
		if (damageType == IDamagable.DamageType.isPoisonDamageType)
		{
			Debug.Log("Poison Dmg res: " + poisonDamageResistance);
			damage -= poisonDamageResistance;
		}
		if (damageType == IDamagable.DamageType.isFireDamageType)
		{
			Debug.Log("Fire Dmg res: " + fireDamageResistance);
			damage -= fireDamageResistance;
		}
		if (damageType == IDamagable.DamageType.isIceDamageType)
		{
			Debug.Log("Ice Dmg res: " + iceDamageResistance);
			damage -= iceDamageResistance;
		}
		else
		{
			Debug.Log("Physical Dmg res: " + physicalDamageResistance);
			 damage -= physicalDamageResistance;
		}

		if (damage < 2) //always deal 2 damage
			damage = 2;

		Debug.Log("health lost after resistance: " + damage);
		currentHealth -= damage;
	}
}
