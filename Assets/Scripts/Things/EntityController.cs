using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class EntityController : MonoBehaviour, IGetStatModifier
{
	public int entityLevel;
	public EntityBaseStatsSO entityBaseStats;
	public float statModifier;

	private Rigidbody rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		GetStatModifier(entityLevel, IGetStatModifier.Rarity.isCommon);
	}

	public virtual void Update()
	{

	}

	public void GetStatModifier(int itemLevel, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		statModifier = modifier + (itemLevel - 1f) / 20;  //get level modifier

		if (GetComponent<EntityHealth>() == null)
			{ Debug.LogWarning("EntityHealth Componenet not found"); return; }
		else
			GetComponent<EntityHealth>().SetHealthStats(entityBaseStats, statModifier);
	}
}
