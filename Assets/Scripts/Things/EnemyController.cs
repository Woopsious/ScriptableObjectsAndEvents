using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : Things
{
	public EnemyType enemyType;
	public enum EnemyType
	{
		isOgre, isSkeleton, isZombie
	}

	public void GetStatModifier(int itemLevel, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		modifier += (float)itemLevel / 20 - 0.025f;  //get enemy level modifier
	}
}
