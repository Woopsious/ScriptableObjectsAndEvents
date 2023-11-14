using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : Entities
{
	public bool isSkeleton;
	public bool isOgre;
	public bool isZombie;

	public override void Init()
	{
		base.Init();

		if (isSkeleton)
		{
			stats = (EntityStatsSO)ScriptableObject.CreateInstance(typeof(EntityStatsSO));
			AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Entities/Enemy_skeleton.asset", typeof(EntityStatsSO));
		}
		if (isOgre)
		{
			stats = (EntityStatsSO)ScriptableObject.CreateInstance(typeof(EntityStatsSO));
			AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Entities/Enemy_skeleton.asset", typeof(EntityStatsSO));
		}
		if (isZombie)
		{
			stats = (EntityStatsSO)ScriptableObject.CreateInstance(typeof(EntityStatsSO));
			AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Entities/Enemy_skeleton.asset", typeof(EntityStatsSO));
		}
	}
}
