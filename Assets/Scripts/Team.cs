using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
	public Material mat;
	public List<Unit> units = new List<Unit>();
	public GameObject unit;
	public GridLoc spawn;

	public void Spawn()
	{
		spawn = GameObject.Find("Island").GetComponent<IslandData>().GetRandom(.5f);
		for(int i = 1; i <= 3; i++)
		{
			GameObject u = Instantiate(unit) as GameObject;
			u.transform.Find("Char").GetComponent<Renderer>().material = mat;
			u.GetComponent<Unit>().gridLoc = spawn.Move(i);
			u.transform.position = spawn.Move(i).ToWorld() + (GameObject.Find("Island").GetComponent<IslandData>().tiles[spawn.Move(i)] * Vector3.up * .25f);
			units.Add(u.GetComponent<Unit>());
			u.transform.parent = transform;
		}
	}

}
