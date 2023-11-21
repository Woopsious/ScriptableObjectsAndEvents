using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class EntityController : MonoBehaviour
{
	public LootPoolsSO lootPool;
	private Rigidbody rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
}
