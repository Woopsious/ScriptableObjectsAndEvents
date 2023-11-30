using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsScriptableObject", menuName = "EnemyBehaviours")]
public class SOEntityBehaviours : ScriptableObject
{
	[Header("Idle Behaviour")]
	public int idleWaitTime;
	[Tooltip("50 is base value")]
	public int idleWanderRadius;

	[Header("Attack Behaviour")]
	[Tooltip("max radial distance from player, 10 is base value")]
	public float aggroRange;

	[Tooltip("max distance till player looses aggro while in view, cant be lower then aggroRange")]
	public float maxChaseRange;
	public float attackRange; //might be useless as weapons store there own attack range

	[Header("Movement Behaviour")]
	public float navMeshMoveSpeed;
	public float navMeshTurnSpeed;
	public float navMeshAcceleration;
}
