using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootPoolScriptableObject", menuName = "LootPools")]
public class LootPoolsSO : ScriptableObject
{
	public int droppedGoldAmount;

	public List<DroppedItems> lootPoolList = new List<DroppedItems>();

	public int minDroppedItemsAmount;
	public int maxDroppedItemsAmount;
}
