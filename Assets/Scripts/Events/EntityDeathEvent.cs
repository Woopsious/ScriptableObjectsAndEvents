using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityDeathEventScriptableObject", menuName = "Events/EntityDeathEvent")]
public class EntityDeathEvent : ScriptableObject
{
	List<EventManager> listeners = new List<EventManager>();

	public void TriggerEvent()
	{
		for (int i = 0; i < listeners.Count; i++)
		{
			listeners[i].OnEventTriggered();
		}
	}

	public void AddListener(EventManager listener)
	{
		listeners.Add(listener);
	}
	public void RemoveListener(EventManager listener)
	{
		listeners.Remove(listener);
	}
}
