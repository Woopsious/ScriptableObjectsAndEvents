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

		gameObject.transform.position = Camera.main.WorldToScreenPoint(EntityObjRef.transform.position + new Vector3(0, 14, 0));
	}
	public void OnRecieveDamage(int maxHealth, int currentHealth)
	{
		ShowUIHealthBar(maxHealth, currentHealth);
	}

	public void ShowUIHealthBar(int maxHealth, int currentHealth)
	{
		gameObject.SetActive(true);
		UpdateHealthBar(maxHealth, currentHealth);
	}
	public void HideUIHealthBar()
	{
		gameObject.SetActive(false);
	}
	public void UpdateHealthBar(int maxHealth, int currentHealth)
	{
		float healthPercentage = (float)currentHealth / maxHealth;
		HealthSlider.value = healthPercentage;
		HealthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
	}
	public void RemoveUi()
	{
		Destroy(gameObject);
	}
}
