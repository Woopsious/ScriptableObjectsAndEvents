using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityLootSpawnHandler : MonoBehaviour
{
	public GameObject droppedItemPrefab;
	public SOLootPools lootPool;
	public SOLootPools lootPoolTest;

	public void SpawnLootOnEntityDeath(Vector3 position)
	{
		for (int i = 0; i < lootPoolTest.minDroppedItemsAmount; i++) //spawn item from loot poll at death location
		{
			///
			/// in future make items have a loot pool weight, higher weight = more common EG: chainmail armor rarer then leather armor etc.
			///

			int index = GetRandomNumber(lootPoolTest.lootPoolList.Count);
			GameObject go = Instantiate(droppedItemPrefab, position, Quaternion.identity);
			Debug.LogWarning(index);
			Debug.LogWarning(lootPoolTest);

			if (lootPoolTest.lootPoolList[index].itemType == SOItems.ItemType.isWeapon)
				SetUpWeaponItem(go, index);

			if (lootPoolTest.lootPoolList[index].itemType == SOItems.ItemType.isArmor)
				SetUpArmorItem(go, index);

			//consumables type check and function call

			SetUpItem(go, index);

			//generic data here, may change if i make unique droppables like keys as they might not have a need for item level etc.
			//im just not sure of a better way to do it atm
			go.AddComponent<Interactables>(); //add interactables script. set randomized stats
			go.GetComponent<Items>().OnItemDrop(SetRarity(), SetItemLevel());
		}
	}
	public void SetUpItem(GameObject go, int index)
	{
		Items item = go.GetComponent<Items>();
		item.gameObject.name = lootPoolTest.lootPoolList[index].name;
		item.itemName = lootPoolTest.lootPoolList[index].name;
		item.itemImage = lootPoolTest.lootPoolList[index].itemImage;
		item.ItemPrice = lootPoolTest.lootPoolList[index].ItemPrice;
	}
	public void SetUpWeaponItem(GameObject go, int index)
	{
		Weapons weapon = go.AddComponent<Weapons>();
		weapon.weaponBaseRef = (SOWeapons)lootPoolTest.lootPoolList[index];
		weapon.currentStackCount = 1;
	}
	public void SetUpArmorItem(GameObject go, int index)
	{
		Armors armor = go.AddComponent<Armors>();
		armor.armorBaseRef = (SOArmors)lootPoolTest.lootPoolList[index];
		armor.currentStackCount = 1;
	}

	//CONSUMABLES SPECIFIC FUNCTION

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
	//return random item lvl in range of player lvl +/- a max of 4
	public int SetItemLevel()
	{
		int playerLvl = FindObjectOfType<PlayerController>().gameObject.GetComponent<EntityHealth>().entityLevel;
		int itemLvl = GetRandomNumber(9);
		itemLvl += playerLvl;

		if (itemLvl <= 0)
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
