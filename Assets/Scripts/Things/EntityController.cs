using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class EntityController : MonoBehaviour, IGetStatModifier
{
	public int entityLevel;
	public EntityBaseStatsSO entityBaseStats;

	private Rigidbody rb;

	public void Start()
	{
		Init();
	}
	public virtual void Update()
	{

	}

	public void Init()
	{
		rb = GetComponent<Rigidbody>();
		GetStatModifier(entityLevel, IGetStatModifier.Rarity.isCommon);
	}
	public void GetStatModifier(int itemLevel, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		modifier += (float)itemLevel / 20 - 0.025f;  //get level modifier

		if (GetComponent<EntityHealth>() == null)
			{ Debug.LogWarning("EntityHealth Componenet not found"); return; }
		else
			GetComponent<EntityHealth>().SetHealthStats(entityBaseStats, modifier);
	}
}
