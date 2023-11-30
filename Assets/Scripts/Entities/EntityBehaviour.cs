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
	public NavMeshAgent navMeshAgent;
	public Rigidbody rb;

	public SOEntityBehaviours entityBehaviour;
	public SphereCollider viewRangeCollider;

	private Bounds idleBounds;
	private Vector3 movePosition;
	private bool HasReachedDestination;

	private float timer;

	public void Start()
	{
		idleBounds.min = new Vector3(transform.position.x - 50, transform.position.y - 3, transform.position.z - 50);
		idleBounds.max = new Vector3(transform.position.x + 50, transform.position.y + 3, transform.position.z + 50);

		HasReachedDestination = true;

		viewRangeCollider.radius = entityBehaviour.aggroRange;

		navMeshAgent.speed = entityBehaviour.navMeshMoveSpeed;
		navMeshAgent.angularSpeed = entityBehaviour.navMeshTurnSpeed;
		navMeshAgent.acceleration = entityBehaviour.navMeshAcceleration;
	}
	public void Update()
	{
		IdleAtPositionTimer();
		CheckDistance();
	}

	//idle Behaviour
	public void IdleAtPositionTimer()
	{
		if (HasReachedDestination == true)
		{
			timer -= Time.deltaTime;

			if (timer < 0)
			{
				timer = entityBehaviour.idleWaitTime;
				FindNewIdlePosition();
			}
		}
	}
	public void FindNewIdlePosition()
	{
		SampleNewIdleMovePosition();
	}
	public void SampleNewIdleMovePosition()
	{
		Vector3 randomMovePosition = Utilities.GetRandomPointInBounds(idleBounds);

		NavMesh.SamplePosition(randomMovePosition, out NavMeshHit navMeshHit, 25, navMeshAgent.areaMask);
		movePosition = navMeshHit.position;

		if (CheckAndSetNewPath(movePosition))
			return;
		else
			FindNewIdlePosition();
	}
	public bool CheckAndSetNewPath(Vector3 movePosition)
	{
		NavMeshPath path = new NavMeshPath();
		if (navMeshAgent.CalculatePath(movePosition, path))
		{
			navMeshAgent.SetPath(path);
			HasReachedDestination = false;
			navMeshAgent.isStopped = false;
			return true;
		}
		else return false;
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
