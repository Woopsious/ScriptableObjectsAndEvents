using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityLootSpawnHandler : MonoBehaviour
{
	public SOLootPools lootPool;

	public void SpawnLootOnEntityDeath()
	{
		for (int i = 0; i < lootPool.minDroppedItemsAmount; i++) //spawn item from loot poll at death location
		{
			GameObject go = Instantiate(lootPool.lootPoolList[GetRandomNumber(lootPool.lootPoolList.Count)].gameObject,
				transform.position, Quaternion.identity);

			go.AddComponent<Interactables>(); //add interactables script. set randomized stats
			go.GetComponent<Items>().OnItemDrop(SetRarity(), SetItemLevel());
		}
	}
	//get stat modifier values
	public Items.Rarity SetRarity()
	{
		float percentage = GetRandomNumber(101);

		if (percentage > 90)
			return Items.Rarity.isLegendary;
		else if (percentage > 60 && percentage < 90)
			return Items.Rarity.isRare;
		else
			return Items.Rarity.isCommon;
	}
	public int SetItemLevel()
	{
		int itemLvl;
		int playerLvl = FindObjectOfType<PlayerController>().gameObject.GetComponent<EntityHealth>().entityLevel;
		itemLvl = playerLvl + GetRandomNumber(9) - 4; //random item lvl in range of player lvl +/- a max of 4

		if (itemLvl >= 0)
			itemLvl = 0;
		return itemLvl - 4;
	}

	public int GetRandomNumber(int num)
	{
		return Random.Range(0, num);
	}
	public bool WillDropExtraloot()
	{
		return false;
	}
}
