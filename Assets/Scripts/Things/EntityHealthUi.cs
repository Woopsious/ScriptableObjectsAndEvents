using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthUi : MonoBehaviour
{
	public GameObject UiObj;
	public Slider HealthSlider;
	public Text HealthText;

	public void Start()
	{
		Init();
	}
	public void Init()
	{
		try
		{
			UiObj.transform.SetParent(FindObjectOfType<Canvas>().gameObject.transform);
			UiObj.transform.rotation = Quaternion.identity;
		}
		catch
		{
			Debug.LogWarning("no Canvas in scene");
		}
	}
	public void Update()
	{
		if (!UiObj.activeInHierarchy)
			return;

		UiObj.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 5, 0));
	}

	public void ShowUIHealthBar(int currentHealth, int maxHealth)
	{
		UiObj.SetActive(true);
		UpdateHealthBar(currentHealth, maxHealth);
	}
	public void HideUIHealthBar()
	{
		UiObj.SetActive(false);
	}
	public void UpdateHealthBar(int currentHealth, int maxHealth)
	{
		float health = currentHealth;
		float healthPercentage = health / maxHealth * 100;
		HealthSlider.value = healthPercentage;
		HealthText.text = health.ToString() + " / " + maxHealth.ToString();
	}
}
