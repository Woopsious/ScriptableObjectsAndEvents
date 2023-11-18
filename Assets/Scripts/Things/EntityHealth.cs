using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
	[Header("Health")]
	public bool isDamagable;
	public int maxHealth;
	public int currentHealth;

	[Header("Resistances")]
	public int physicalDamageResistance;
	public int poisonDamageResistance;
	public int fireDamageResistance;
	public int iceDamageResistance;

	[Header("Optional Refs")]
	public EntityHealthUi healthUi;

	public void Start()
	{
		Init();
	}

	//set up functions
	public void Init()
	{
		if (GetComponent<EntityController>() == null)
			{ Debug.LogWarning("EntityContriller Component Not Found"); return; }
		else
			SetHealthStats(GetComponent<EntityController>().entityBaseStats, 1f);
	}
	public void SetHealthStats(EntityBaseStatsSO entityBaseStats, float modifier)
	{
		maxHealth = (int)(entityBaseStats.maxHealth * modifier);
		currentHealth = (int)(entityBaseStats.maxHealth * modifier);
		physicalDamageResistance = (int)(entityBaseStats.physicalDamageResistance * modifier);
		poisonDamageResistance = (int)(entityBaseStats.poisonDamageResistance * modifier);
		fireDamageResistance = (int)(entityBaseStats.fireDamageResistance * modifier);
		iceDamageResistance = (int)(entityBaseStats.iceDamageResistance * modifier);
	}

	//health functions
	public virtual void RecieveHealing(int health)
	{
		currentHealth += health;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}
	public void RecieveDamage(int damage, IDamagable.DamageType damageType)
	{
		Debug.Log(gameObject.name + " recieved :" + damage);
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

		currentHealth -= damage;
		healthUi.UpdateHealthBar(currentHealth, maxHealth);	//ui not made atm
		Debug.Log("health lost after resistance: " + damage + " | current health: " + currentHealth);
	}
}
