using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int playerLevel;
	public EntityBaseStatsSO entityBaseStats;

	public GameObject weaponContainer;
	public Weapons equippedWeapon;

	private Rigidbody rb;

	public void Start()
	{
		Init();
	}
	public void Update()
	{
		PlayerMovementKeys();
	}

	public void Init()
	{
		rb = GetComponent<Rigidbody>();
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
