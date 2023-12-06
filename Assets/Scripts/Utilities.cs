using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Utilities
{
	public static int GetRandomNumber(int num)
	{
		return Random.Range(0, num);
	}
	public static Vector3 GetRandomPointInBounds(Bounds bounds)
	{
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z));
	}
	//return random rarity
	public static Items.Rarity SetRarity()
	{
		float percentage = GetRandomNumber(101);

		if (percentage > 90)
			return Items.Rarity.isLegendary;
		else if (percentage > 60 && percentage < 90)
			return Items.Rarity.isRare;
		else
			return Items.Rarity.isCommon;
	}
	//return random item lvl in range of player lvl +/- a max of 4
	public static int SetItemLevel(int level)
	{
		int entityLvl = level;
		int itemLvl = GetRandomNumber(9);
		itemLvl += entityLvl;

		if (itemLvl <= 0)
			itemLvl = 0;
		return itemLvl - 4;
	}
}
