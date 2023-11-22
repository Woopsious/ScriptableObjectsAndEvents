using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityBehaviour : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	private Rigidbody rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
}
