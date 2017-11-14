using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
	public List<Unit> initative = new List<Unit>();
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
			initative.Add(u);
		}
		Select();
	}

	void NextUnit()
	{
		Unit u = initative[0];
		initative.RemoveAt(0);
		initative.Add(u);
		Select();
	}

	void Select()
	{
		selection.transform.position = initative[0].transform.position;
		CameraControl.Instance.FocusCamera(initative[0].gridLoc);
	}
}