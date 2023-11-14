using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemsScriptableObject", menuName = "Items")]
public class ItemsSO : ScriptableObject
{
	public Image itemImage;
	public int ItemPrice;

	[Header("Item toggles")]
	public bool isConsumable;
	public bool isEquipable;

	[Header("Is Inventory Stackable")]
	public bool isStackable;
	public int MaxStackCount;
}
