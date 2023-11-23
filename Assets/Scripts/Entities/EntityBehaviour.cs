using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EntityBehaviour : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	private Rigidbody rb;

	public List<Weapons> possibleWeaponsList = new List<Weapons>();
	public GameObject weaponContainer;
	public Weapons equippedWeapon;

	/// <summary>
	/// have equipped armor slots here as well that differ slightly from weapons as armor will just add to resistances
	/// where weapons will actually exist in the game world for my planned 2D Dungeon crawler
	/// public List<Armor> possibleHelmetsList = new List<Armor>();
	/// public Armor equippedHelmet
	/// public List<Armor> possibleChestPiecesList = new List<Armor>();
	/// public Armor equippedChestPiece
	/// public List<Armor> possibleLegPiecesList = new List<Armor>();
	/// public Armor equippedLegPiece
	/// </summary>

	public float timer;
	public float timerCooldown = 10f;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	public void Update()
	{
		timer -= Time.deltaTime;

		if (timer < 0)
		{
			timer = timerCooldown;

			//Debug.LogWarning(equippedWeapon.damage);
			//Debug.LogWarning(equippedWeapon.itemLevel);
			//Debug.LogWarning(equippedWeapon.rarity);
		}
	}
	public void SetUpWeaponObj(GameObject go)
	{
		Destroy(weaponContainer.transform.GetChild(0).gameObject);
		go = Instantiate(possibleWeaponsList[GetRandomNumber(possibleWeaponsList.Count)].gameObject, weaponContainer.transform, false);

		go.GetComponent<Weapons>().isEquippedByNonPlayer = true;
		go.transform.localPosition = Vector3.zero;
		Destroy(go.GetComponent<Interactables>()); //on dropped item spawn the interactables script will be added
	}
	public int GetRandomNumber(int num)
	{
		return UnityEngine.Random.Range(0, num);
	}

	/// <summary>
	/// have functions that loop through possible armor and weapons then 
	/// </summary>
}
