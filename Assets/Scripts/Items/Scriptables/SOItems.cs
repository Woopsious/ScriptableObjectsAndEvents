using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemsScriptableObject", menuName = "Items")]
public class SOItems : ScriptableObject
{
	[Header("Item Info")]
	public Image itemImage;
	public int ItemPrice;
	public int ItemId;

	[Header("Item Type")]
	public ItemType itemType;
	public enum ItemType
	{
		isConsumable, isWeapon, isArmor
	}
	public bool isEquipable;

	[Header("Is Inventory Stackable")]
	public bool isStackable;
	public int MaxStackCount;
}
