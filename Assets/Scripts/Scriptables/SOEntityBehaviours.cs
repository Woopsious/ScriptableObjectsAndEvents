using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsScriptableObject", menuName = "EnemyBehaviours")]
public class SOEntityBehaviours : ScriptableObject
{
	[Header("Attack Behaviour")]
	public int idleWaitTime;

	[Tooltip("10 is base value")]
	public float aggroRange;

	[Tooltip("will follow player as long as they are in view")]
	public float maxChaseRange;
	public float attackRange; //might be useless as weapons store there own attack range

	[Header("Movement Behaviour")]
	public float navMeshMoveSpeed;
	public float navMeshTurnSpeed;
	public float navMeshAcceleration;
}
