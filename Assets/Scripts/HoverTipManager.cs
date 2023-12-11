using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour
{
	public RectTransform tipWindow;
	public TextMeshProUGUI tipText;

	public static Action<string, Vector2> OnMouseHover;
	public static Action OnMouseLoseFocus;

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	private void Start()
	{
		HideTip();
	}

	private void ShowTip(string tip, Vector2 mousePos)
	{
		tipText.text = tip;
		tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);

		tipWindow.gameObject.SetActive(true);
		tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);
	}

	private void HideTip()
	{
		tipText.text = null;
		tipWindow.gameObject.SetActive(false);
	}
}
