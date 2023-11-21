using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSpawner : MonoBehaviour
{
	public void SpawnLootOnEntityDeath(GameObject obj)
	{
		if (obj.GetComponent<EntityController>() == null) return; //if comp exists grap lootpoll + create local refs

		LootPoolsSO entityLootPool = obj.GetComponent<EntityController>().lootPool;
		Vector3 position = obj.transform.position;

		for (int i = 0; i < entityLootPool.minDroppedItemsAmount; i++) //spawn item from loot poll at death location
		{
			GameObject go = Instantiate(entityLootPool.lootPoolList[GetRandomNumber(entityLootPool.lootPoolList.Count)].gameObject,
				position, Quaternion.identity);

			go.AddComponent<Interactables>(); //add interactables script. set randomized stats
			go.GetComponent<DroppedItems>().OnItemDrop(SetRarity(), SetItemLevel());
		}
	}
	public DroppedItems.Rarity SetRarity()
	{
		float percentage = GetRandomNumber(100);

		if (percentage > 90)
			return DroppedItems.Rarity.isLegendary;
		else if (percentage > 60 && percentage < 90)
			return DroppedItems.Rarity.isRare;
		else
			return DroppedItems.Rarity.isCommon;
	}
	public int SetItemLevel()
	{
		int itemLvl;
		int playerLvl = FindObjectOfType<PlayerController>().gameObject.GetComponent<EntityHealth>().entityLevel;
		itemLvl = playerLvl + GetRandomNumber(8) - 4; //random item lvl in range of player lvl +/- a max of 4

		if (itemLvl >= 0)
			itemLvl = 0;
		return itemLvl - 4;
	}


	public int GetRandomNumber(int num)
	{
		return Random.Range(0, num + 1);
	}

	public bool WillDropExtraloot()
	{
		return false;
	}
}
