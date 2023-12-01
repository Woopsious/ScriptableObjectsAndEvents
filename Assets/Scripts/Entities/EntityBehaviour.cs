using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityBehaviour : MonoBehaviour
{
	public LayerMask includeMe;
	public NavMeshAgent navMeshAgent;
	public Rigidbody rb;

	public SOEntityBehaviours entityBehaviour;
	public SphereCollider viewRangeCollider;
	public GameObject centerPoint;

	private Bounds idleBounds;
	private Vector3 movePosition;
	private bool HasReachedDestination;

	private float idleTimer;
	public float chaseTimer;
	public float chaseTimerCooldown = 1f;

	public PlayerController player;
	public Vector3 playersLastKnownPosition;
	private Bounds chaseBounds;

	public void Start()
	{
		idleBounds.min = new Vector3( transform.position.x - entityBehaviour.idleWanderRadius, 
			transform.position.y - 3, transform.position.z - entityBehaviour.idleWanderRadius);

		idleBounds.max = new Vector3(transform.position.x + entityBehaviour.idleWanderRadius, 
			transform.position.y + 3, transform.position.z + entityBehaviour.idleWanderRadius);

		chaseBounds.min = new Vector3(transform.position.x - entityBehaviour.maxChaseRange,
			transform.position.y - 3, transform.position.z - entityBehaviour.maxChaseRange);

		chaseBounds.max = new Vector3(transform.position.x + entityBehaviour.maxChaseRange,
			transform.position.y + 3, transform.position.z + entityBehaviour.maxChaseRange);

		HasReachedDestination = true;

		viewRangeCollider.radius = entityBehaviour.aggroRange;
		viewRangeCollider.gameObject.GetComponent<PlayerDetection>().SetBehaviourRef(this);

		navMeshAgent.speed = entityBehaviour.navMeshMoveSpeed;
		navMeshAgent.angularSpeed = entityBehaviour.navMeshTurnSpeed;
		navMeshAgent.acceleration = entityBehaviour.navMeshAcceleration;
	}
	public void Update()
	{
		if (player == null)
		{
			IdleAtPositionTimer();
			CheckIdleDistance();
		}
		else
		{
			ChasePlayer();
			//do some other shit
		}
	}

	//idle behaviour
	public void IdleAtPositionTimer()
	{
		if (HasReachedDestination == true)
		{
			idleTimer -= Time.deltaTime;

			if (idleTimer < 0)
			{
				idleTimer = entityBehaviour.idleWaitTime;
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
	public void CheckIdleDistance()
	{
		if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && HasReachedDestination == false)
		{
			HasReachedDestination = true;
		}
	}

	//attack behaviour
	public void ChasePlayer()
	{
		//if player != null and player in view, start chase

		if (CheckChaseDistance())
		{
			MoveTowardsPlayer();
			Debug.LogWarning("Player visible");
		}
		else
		{
			player = null;
			Debug.LogWarning("Player not visible");
		}
	}
	public void MoveTowardsPlayer()
	{
		//if player distance greater then maxChaseRange (checked in CheckDistance), stop chasing get new idlePosition, reset player ref
		//every x amount of time, sample player position and move towards it

		UpdatePlayerPosition();
		Vector3 movePosition = SampleNewMovePosition(player.centerPoint.transform.position);
		CheckAndSetNewPath(movePosition);
	}
	public void UpdatePlayerPosition()
	{
		//if player in view update player position else return.
		if (CheckIfPlayerVisible())
			playersLastKnownPosition = player.transform.position;
	}
	public bool CheckIfPlayerVisible()
	{
		//raycast check to see if player in view, return true or false.
		Physics.Linecast(centerPoint.transform.position, player.centerPoint.transform.position, out RaycastHit hit, includeMe);

		//occasionally throws null i have no clue why, code is basically a copy and paste from previous game
		//dont remember null error happening in last game at all, it doesnt seem to break anything at all either
		try
		{
			if (hit.point != null && hit.collider.gameObject == player.gameObject)
				return true;
			else
				return false;
		}
		catch
		{
			return false;
		}
	}
	public bool CheckChaseDistance()
	{
		if (player != null && navMeshAgent.remainingDistance > entityBehaviour.maxChaseRange)
			return false;
		else return true;
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

	//utility
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(idleBounds.center, idleBounds.size);

		if (player != null)
			Gizmos.DrawLine(centerPoint.transform.position, player.centerPoint.transform.position);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(chaseBounds.center, chaseBounds.size);
	}
}
