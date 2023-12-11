using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool isTip;

	public string tipToShow;
	private float timeToWait = 0.5f;
	public void OnPointerEnter(PointerEventData eventData)
	{
		StopAllCoroutines();
		StartCoroutine(StartTimer());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		StopAllCoroutines();
		HoverTipManager.OnMouseLoseFocus();
	}

	private void ShowMessage()
	{

	}
	public IEnumerator StartTimer()
	{
		yield return new WaitForSeconds(timeToWait);
		ShowMessage();
	}
}
