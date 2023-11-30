using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EntityBehaviour : MonoBehaviour
{
	public Bounds idleBounds;
	public Vector3 movePosition;
	public bool HasReachedDestination;

	public NavMeshAgent navMeshAgent;
	public Rigidbody rb;

	public float timer;
	public float timerCooldown = 10f;

	public void Start()
	{
		idleBounds.min = new Vector3(transform.position.x - 50, transform.position.y - 3, transform.position.z - 50);
		idleBounds.max = new Vector3(transform.position.x + 50, transform.position.y + 3, transform.position.z + 50);

		HasReachedDestination = true;
	}
	public void Update()
	{
		IdleAtPosition();
		CheckDistance();
	}
	public void IdleAtPosition()
	{
		if (HasReachedDestination == true)
		{
			timer -= Time.deltaTime;

			if (timer < 0)
			{
				timer = timerCooldown;
				FindNewIdlePosition();
			}
		}
	}
	public void FindNewIdlePosition()
	{
		SampleNewMovePosition();

		Debug.LogWarning("position found");
		HasReachedDestination = false;
	}
	public bool SampleNewMovePosition()
	{
		Vector3 randomMovePosition = Utilities.GetRandomPointInBounds(idleBounds);

		NavMesh.SamplePosition(randomMovePosition, out NavMeshHit navMeshHit, 25, navMeshAgent.areaMask);
		movePosition = navMeshHit.position;

		if (CheckAndSetNewPath(movePosition))
			return true;
		else return false;
	}
	public bool CheckAndSetNewPath(Vector3 movePosition)
	{
		NavMeshPath path = new NavMeshPath();
		if (navMeshAgent.CalculatePath(movePosition, path))
		{
			navMeshAgent.SetPath(path);
			navMeshAgent.isStopped = false;
			return true;
		}
		else
		{
			return false;
		}
	}
	public void CheckDistance()
	{
		if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && HasReachedDestination == false)
		{
			HasReachedDestination = true;
			navMeshAgent.isStopped = true;
		}
	}
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(idleBounds.center, idleBounds.size);
	}
}
