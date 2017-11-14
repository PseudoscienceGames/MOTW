using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPanel : MonoBehaviour
{
	public GameObject charPortrait;

	public void AddPanels(List<Unit> units)
	{
		foreach (Transform child in transform)
			Destroy(child.gameObject);
		foreach(Unit unit in units)
		{
			GameObject currentPortrait  = Instantiate(charPortrait) as GameObject;
			currentPortrait.transform.SetParent(transform);
			currentPortrait.GetComponent<Image>().sprite = unit.charPortrait;
		}
	}
}
