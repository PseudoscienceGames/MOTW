using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
	public Material mat;
	public List<Unit> units = new List<Unit>();
	public GameObject unit;

	private void Start()
	{
		for(int i = 1; i <= 3; i++)
		{
			Instantiate(unit);
		}
	}

}
