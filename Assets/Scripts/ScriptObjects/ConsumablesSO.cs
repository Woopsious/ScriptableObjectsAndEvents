using UnityEngine;

[CreateAssetMenu(fileName = "ConsumablesScriptableObject", menuName = "Items/Consumables")]
public class ConsumablesSO : ItemsSO
{
	[Header("Consumable Info")]
	public int healthRestoration;
	public int manaRestoration;
}
