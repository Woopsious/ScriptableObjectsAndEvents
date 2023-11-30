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

	public PlayerController player;
	public Vector3 playersLastKnownPosition;

	public void Start()
	{
		idleBounds.min = new Vector3( transform.position.x - entityBehaviour.idleWanderRadius, 
			transform.position.y - 3, transform.position.z - entityBehaviour.idleWanderRadius);

		idleBounds.max = new Vector3(transform.position.x + entityBehaviour.idleWanderRadius, 
			transform.position.y + 3, transform.position.z + entityBehaviour.idleWanderRadius);

		HasReachedDestination = true;

		viewRangeCollider.radius = entityBehaviour.aggroRange;

		navMeshAgent.speed = entityBehaviour.navMeshMoveSpeed;
		navMeshAgent.angularSpeed = entityBehaviour.navMeshTurnSpeed;
		navMeshAgent.acceleration = entityBehaviour.navMeshAcceleration;
	}
	public void Update()
	{
		if (player == null)
		{
			IdleAtPositionTimer();
			CheckDistance();
		}
		else
		{
			UpdatePlayerPosition();
			//do some other shit
		}
	}
	public void ChasePlayer()
	{
		//if player != null and player in view, start chase
	}

	//idle behaviour
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
		movePosition = SampleNewMovePosition(randomMovePosition);

		if (CheckAndSetNewPath(movePosition))
			return;
		else
			FindNewIdlePosition();
	}

	//attack behaviour
	public void MoveTowardsPlayer()
	{
		//if player distance greater then maxChaseRange, stop chasing and get new idlePosition, reset player ref, 

		//every x amount of time, sample player position and move towards it
	}
	public void UpdatePlayerPosition()
	{
		//if player in view update player position else return.
		CheckIfPlayerVisible();

		playersLastKnownPosition = player.transform.position;
	}
	public void CheckIfPlayerVisible()
	{
		//raycast check to see if player in view, return true or false.
	}

	//idle + attack behaviour
	public Vector3 SampleNewMovePosition(Vector3 position)
	{
		NavMesh.SamplePosition(position, out NavMeshHit navMeshHit, 10, navMeshAgent.areaMask);
		return navMeshHit.position;
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
		Gizmos.DrawWireCube(idleBounds.center, idleBounds.size);
	}
}
