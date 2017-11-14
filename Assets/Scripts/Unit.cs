using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public GridLoc gridLoc;
	public int blood;
	public int maxEnegry;
	public int energy;
	public int speed;
	public List<PawnAction> actions = new List<PawnAction>();
	public Sprite charPortrait;

	void Start()
	{
		foreach (PawnAction p in GetComponents<PawnAction>())
		{
			actions.Add(p);
		}
		energy = maxEnegry;
	}

	public void RegenEnergy()
	{
		if (energy < maxEnegry)
			energy++;
	}

	public void BeginTurn()
	{

	}

	public void EndTurn()
	{
		energy = 0;
	}

}
