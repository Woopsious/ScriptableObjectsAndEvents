using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public static PlayerInventory Instance;

	public GameObject weaponContainer;

	public List<DroppedItems> playerInventory = new List<DroppedItems>();

	public List<GameObject> WeaponDatabase = new List<GameObject>();

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

	public void AddItemToPlayerInventory(DroppedItems item)
	{
		playerInventory.Add(item);

		foreach (DroppedItems itemInInventry in playerInventory)
		{
			if (itemInInventry.itemType == DroppedItems.ItemType.isWeapon)
			{
				Debug.Log("item damage: " + itemInInventry.weaponBaseRef.baseDamage);
				Debug.Log("item added to Inventroy: " + itemInInventry.weaponBaseRef);
			}
		}
	}
	public void RemoveItemToPlayerInventory(DroppedItems item)
	{

	}

	public void EquipRandomWeaponInInventroy()
	{
		for (int i = 0; i < 1000; i++)
		{
			int index = GetRandomNumber();

			if (playerInventory[index].itemType == DroppedItems.ItemType.isWeapon)
			{
				GameObject go;
				switch (playerInventory[index].weaponBaseRef.baseDamage)
				{
					case 35:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(playerInventory[index].gameObject, weaponContainer.transform, false);
					SetUpWeaponObj(go);
					break;
					case 25:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(playerInventory[index].gameObject, weaponContainer.transform, false);
					SetUpWeaponObj(go);
					break;
					case 8:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(playerInventory[index].gameObject, weaponContainer.transform, false);
					SetUpWeaponObj(go);
					break;
					case 30:
					Destroy(weaponContainer.transform.GetChild(0).gameObject);
					go = Instantiate(playerInventory[index].gameObject, weaponContainer.transform, false);
					SetUpWeaponObj(go);
					break;
				}
				return;
			}
		}
	}
	public void SetUpWeaponObj(GameObject go)
	{
		go.GetComponent<Weapons>().isEquipped = true;
		go.transform.localPosition = Vector3.zero;
		Destroy(go.GetComponent<Interactables>()); //on dropped item spawn the interactables script will be added
	}
	public int GetRandomNumber()
	{
		int i = Random.Range(0, playerInventory.Count);
		return i;
	}
}
