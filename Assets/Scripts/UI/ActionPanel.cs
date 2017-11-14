using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour
{
	public GameObject actionButton;

	public void AddButtons(Unit unit)
	{
		foreach (Transform child in transform)
			Destroy(child.gameObject);
		foreach (PawnAction action in unit.actions)
		{
			GameObject button = Instantiate(actionButton) as GameObject;
			button.transform.SetParent(transform);
			button.transform.GetChild(0).GetComponent<Text>().text = action.actionName;
			button.GetComponent<Button>().onClick.AddListener(delegate { action.SpawnWidget(); });
		}
	}
}