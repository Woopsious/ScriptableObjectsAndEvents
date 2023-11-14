using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entities
{
	public int playerLevel;

	public override void Update()
	{
		base.Update();

		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * stats.moveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector3.left * stats.moveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * stats.moveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * stats.moveSpeed * Time.deltaTime);
		}

		//Debug.Log(stats.moveSpeed);

		if (Input.GetKey(KeyCode.Q))
		{
			transform.eulerAngles -= new Vector3(transform.rotation.x, stats.turnSpeed, transform.rotation.y) * Time.unscaledDeltaTime;
		}
		if (Input.GetKey(KeyCode.E))
		{
			transform.eulerAngles -= new Vector3(transform.rotation.x, -stats.turnSpeed, transform.rotation.y) * Time.unscaledDeltaTime;
		}
	}
}
