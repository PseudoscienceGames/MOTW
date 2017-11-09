using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public IslandData iD;
	public IslandMesh iM;
	public List<Team> teams;

	private void Start()
	{
		iD.GenIsland();
		foreach(Team t in teams)
		{
			t.Spawn();
		}
		GetComponent<InitManager>().InitInit();
	}
}
