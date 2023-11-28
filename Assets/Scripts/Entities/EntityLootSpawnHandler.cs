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

	//invoked from event
	public void OnDeathEvent(Vector3 position)
	{
		for (int i = 0; i < lootPool.minDroppedItemsAmount; i++) //spawn item from loot poll at death location
		{
			///
			/// in future make items have a loot pool weight, higher weight = more common EG: chainmail armor rarer then leather armor etc.
			///

			int index = Utilities.GetRandomNumber(lootPool.lootPoolList.Count);
			GameObject go = Instantiate(droppedItemPrefab, position, Quaternion.identity);

			if (lootPool.lootPoolList[index].itemType == SOItems.ItemType.isWeapon)
				SetUpWeaponItem(go, index);

			if (lootPool.lootPoolList[index].itemType == SOItems.ItemType.isArmor)
				SetUpArmorItem(go, index);

			if (lootPool.lootPoolList[index].itemType == SOItems.ItemType.isConsumable)
				SetUpConsumableItem(go, index);

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
		item.gameObject.name = lootPool.lootPoolList[index].name;
		item.itemName = lootPool.lootPoolList[index].name;
		item.itemImage = lootPool.lootPoolList[index].itemImage;
		item.ItemPrice = lootPool.lootPoolList[index].ItemPrice;
	}
	public void SetUpWeaponItem(GameObject go, int index)
	{
		Weapons weapon = go.AddComponent<Weapons>();
		weapon.weaponBaseRef = (SOWeapons)lootPool.lootPoolList[index];
		weapon.currentStackCount = 1;
	}
	public void SetUpArmorItem(GameObject go, int index)
	{
		Armors armor = go.AddComponent<Armors>();
		armor.armorBaseRef = (SOArmors)lootPool.lootPoolList[index];
		armor.currentStackCount = 1;
	}
	public void SetUpConsumableItem(GameObject go, int index)
	{
		Consumables consumables = go.AddComponent<Consumables>();
		consumables.consumableBaseRef = (SOConsumables)lootPool.lootPoolList[index];
		consumables.currentStackCount = 3;
	}

	//return random rarity
	public Items.Rarity SetRarity()
	{
		float percentage = Utilities.GetRandomNumber(101);

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
		int itemLvl = Utilities.GetRandomNumber(9);
		itemLvl += playerLvl;

		if (itemLvl <= 0)
			itemLvl = 0;
		return itemLvl - 4;
	}

	public bool WillDropExtraloot()
	{
		return false;
	}
}
