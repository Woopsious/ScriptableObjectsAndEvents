using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootPoolScriptableObject", menuName = "LootPools")]
public class SOLootPools : ScriptableObject
{
	public int droppedGoldAmount;

	public List<Items> lootPoolList = new List<Items>();

	public int minDroppedItemsAmount;
	public int maxDroppedItemsAmount;
}
