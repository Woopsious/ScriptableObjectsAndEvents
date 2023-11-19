using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IGetStatModifier
{
	public int playerLevel;
	public EntityBaseStatsSO entityBaseStats;
	public float statModifier;

	public GameObject weaponContainer;
	public Weapons equippedWeapon;

	private Rigidbody rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
		GetStatModifier(playerLevel, IGetStatModifier.Rarity.isCommon);
		GetComponent<EntityHealth>().SetHealthStats(entityBaseStats, statModifier);
	}
	public void Update()
	{
		PlayerMovementKeys();
	}

	public void GetStatModifier(int itemLevel, IGetStatModifier.Rarity rarity)
	{
		float modifier = 1f;
		if (rarity == IGetStatModifier.Rarity.isLegendary) { modifier += 0.25f; } //get rarity modifier
		if (rarity == IGetStatModifier.Rarity.isRare) { modifier += 0.1f; }
		else { modifier += 0; }

		statModifier = modifier + (itemLevel - 1f) / 20;  //get level modifier
	}
	public void PlayerMovementKeys()
	{
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * entityBaseStats.navMeshMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector3.left * entityBaseStats.navMeshMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * entityBaseStats.navMeshMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * entityBaseStats.navMeshMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Q))
		{
			transform.eulerAngles -= new Vector3(transform.rotation.x, entityBaseStats.navMeshTurnSpeed, transform.rotation.y) * Time.unscaledDeltaTime;
		}
		if (Input.GetKey(KeyCode.E))
		{
			transform.eulerAngles -= new Vector3(transform.rotation.x, -entityBaseStats.navMeshTurnSpeed, transform.rotation.y) * Time.unscaledDeltaTime;
		}
	}
}
