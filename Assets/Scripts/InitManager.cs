using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
	public List<Unit> initiative = new List<Unit>();
	private GameObject selection;
	public bool next;

	void Update()
	{
		if(next)
		{
			next = false;
			NextUnit();
		}
	}

	public void InitInit()
	{
		selection = GameObject.Find("Selection");
		foreach (Unit u in GameObject.FindObjectsOfType<Unit>())
		{
			initiative.Add(u);
		}
	}

	void NextUnit()
	{
		Unit u = initiative[0];
		initiative.RemoveAt(0);
		initiative.Add(u);
		Select();
	}

	void Select()
	{
		selection.transform.position = initiative[0].transform.position;
		CameraControl.Instance.FocusCamera(initiative[0].gridLoc);
		GameObject.Find("Action Panel").GetComponent<ActionPanel>().AddButtons(initiative[0]);
		GameObject.Find("Init Panel").GetComponent<InitPanel>().AddPanels(initiative);

	}
}