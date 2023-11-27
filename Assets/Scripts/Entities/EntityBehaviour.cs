using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EntityBehaviour : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	private Rigidbody rb;

	public float timer;
	public float timerCooldown = 10f;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	public void Update()
	{
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			timer = timerCooldown;

			//Debug.LogWarning(equippedWeapon.damage);
			//Debug.LogWarning(equippedWeapon.itemLevel);
			//Debug.LogWarning(equippedWeapon.rarity);
		}
	}
}
