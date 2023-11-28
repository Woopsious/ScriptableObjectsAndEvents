using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : Items
{
	[Header("Consumable Info")]
	public int healthRestoration;
	public int manaRestoration;

	public override void Start()
	{
		base.Start();
		SetConsumablesStats();
		isStackable = consumableBaseRef.isStackable;
	}
	public void SetConsumablesStats()
	{
		healthRestoration = consumableBaseRef.healthRestoration;
		manaRestoration = consumableBaseRef.manaRestoration;
	}
}
