using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject weaponContainer;
	public Weapons equippedWeapon;

	private Rigidbody rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	public void Update()
	{
		PlayerMovementKeys();
	}

	public void PlayerMovementKeys()
	{
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * 50 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector3.left * 50 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * 50 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * 50 * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.Q))
		{
			transform.eulerAngles -= new Vector3(transform.rotation.x, 80, transform.rotation.y) * Time.unscaledDeltaTime;
		}
		if (Input.GetKey(KeyCode.E))
		{
			transform.eulerAngles -= new Vector3(transform.rotation.x, -80, transform.rotation.y) * Time.unscaledDeltaTime;
		}
	}
}
