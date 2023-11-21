using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
	public EntityDeathEvent entityDeathEvent;
	public UnityEvent onEventTriggered;

	public void OnEnable()
	{
		entityDeathEvent.AddListener(this);
	}
	public void OnDisable()
	{
		entityDeathEvent.RemoveListener(this);
	}

	public void OnEventTriggered()
	{
		onEventTriggered.Invoke();
	}
}
