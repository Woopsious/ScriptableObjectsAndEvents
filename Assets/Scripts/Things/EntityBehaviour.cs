using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityBehaviour : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;

	public void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
}
