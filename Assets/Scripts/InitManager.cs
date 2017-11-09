using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
	public List<Unit> initative = new List<Unit>();

	public void InitInit()
	{
		foreach(Unit u in GameObject.FindObjectsOfType<Unit>())
		{
			initative.Add(u);
		}
	}
}