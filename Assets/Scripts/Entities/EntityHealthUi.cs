using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthUi : MonoBehaviour
{
	public GameObject EntityObjRef;
	public Slider HealthSlider;
	public TMP_Text HealthText;

	public void Start()
	{
		try
		{
			gameObject.transform.SetParent(FindObjectOfType<Canvas>().gameObject.transform);
			gameObject.transform.rotation = Quaternion.identity;
		}
		catch
		{
			Debug.LogWarning("no Canvas in scene");
		}
	}
	public void Update()
	{
		if (!gameObject.activeInHierarchy)
			return;

		gameObject.transform.position = Camera.main.WorldToScreenPoint(EntityObjRef.transform.position + new Vector3(0, 5, 0));
	}
	public void OnRecieveDamage(int maxHealth, int currentHealth)
	{
		UpdateHealthBar(maxHealth, currentHealth);
	}

	public void ShowUIHealthBar(int maxHealth, int currentHealth)
	{
		gameObject.SetActive(true);
		UpdateHealthBar(currentHealth, maxHealth);
	}
	public void HideUIHealthBar()
	{
		gameObject.SetActive(false);
	}
	public void UpdateHealthBar(int maxHealth, int currentHealth)
	{
		float health = currentHealth;
		float healthPercentage = health / maxHealth * 100;
		HealthSlider.value = healthPercentage;
		HealthText.text = health.ToString() + " / " + maxHealth.ToString();
	}
	public void RemoveUi()
	{
		Destroy(gameObject);
	}
}
