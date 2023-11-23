using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;

	public GameObject weaponContainer;

	public List<ItemData> playerInventory = new List<ItemData>();

	public void Start()
	{
		Instance = this;
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			EquipRandomWeaponInInventroy();
		}
	}

	public void AddItemToPlayerInventory(Items item)
	{
		ItemData itemData = new()
		{
			itemLevel = item.itemLevel,
			itemType = (ItemData.ItemType)item.itemType,
			rarity = (ItemData.Rarity)item.rarity,
			weaponBaseRef = item.weaponBaseRef,
			consumableBaseRef = item.consumableBaseRef,
			currentStackCount = item.currentStackCount,
		};

		playerInventory.Add(itemData);
	}
	public void RemoveItemFromPlayerInventory(Items item)
	{

	}

	public void EquipRandomWeaponInInventroy()
	{
		for (int i = 0; i < 1000; i++)
		{
			int index = GetRandomNumber();

			if (playerInventory[index].itemType == ItemData.ItemType.isWeapon)
			{
				GameObject go;
				switch (playerInventory[index].weaponBaseRef.baseDamage)
				{
					case 35:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
					case 25:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
					case 8:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
					case 30:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(GameManager.Instance.weaponDataBase[index], weaponContainer.transform, false);
					SetUpWeaponObj(go, playerInventory[index]);
					break;
				}
				return;
			}
		}
	}
	public void SetUpWeaponObj(GameObject go, ItemData data)
	{
		Weapons weapon = go.GetComponent<Weapons>();

		weapon.itemLevel = data.itemLevel;
		weapon.itemType = (Items.ItemType)data.itemType;
		weapon.rarity = (Items.Rarity)data.rarity;
		weapon.weaponBaseRef = data.weaponBaseRef;
		weapon.consumableBaseRef = data.consumableBaseRef;

		go.GetComponent<Weapons>().isEquippedByPlayer = true;
		go.transform.localPosition = Vector3.zero;
		Destroy(go.GetComponent<Interactables>()); //on dropped item spawn the interactables script will be added
	}
	public int GetRandomNumber()
	{
		return Random.Range(0, playerInventory.Count);
	}
}
