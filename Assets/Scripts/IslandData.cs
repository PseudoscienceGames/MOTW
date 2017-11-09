using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandData : MonoBehaviour
{
	public int tileCount;
	public Dictionary<GridLoc, int> tiles = new Dictionary<GridLoc, int>();

	public void GenIsland()
	{
		List<GridLoc> possible = new List<GridLoc>();
		tiles.Add(new GridLoc(0, 0), 1);
		for(int i = 0; i <= 5; i++)
		{
			possible.Add(new GridLoc(0, 0).Move(i));
		}
		while(tiles.Count + possible.Count < tileCount)
		{
			GridLoc g = possible[Random.Range(possible.Count / 2, possible.Count - 1)];
			tiles.Add(g, 1);
			possible.Remove(g);
			for (int i = 0; i <= 5; i++)
			{
				if (!possible.Contains(g.Move(i)) && !tiles.ContainsKey(g.Move(i)))
				{
					possible.Add(g.Move(i));
				}
			}
		}
		foreach(GridLoc g in possible)
		{
			tiles.Add(g, 0);
		}
		List<GridLoc> raise = new List<GridLoc>(tiles.Keys);
		int t = 0;
		while (raise.Count > 0)
		{
			t++;
			List<GridLoc> raiseNext = new List<GridLoc>();
			for (int i = 0; i < raise.Count; i++)
			{
				GridLoc g = raise[i];
				bool innard = true;
				for (int j = 0; j < 6; j++)
				{
					if (!raise.Contains(g.Move(j)))
						innard = false;
				}
				if (innard)// && Random.Range(0, 1000) != 0)
				{
					//if(t % 2 == 0)
					tiles[g]++;//= Random.Range(1,3);
					raiseNext.Add(g);
				}
			}
			raise = raiseNext;
		}
		GetComponent<IslandMesh>().GenMesh(tiles);
		if (GetComponent<DrawPaths>())
			GetComponent<DrawPaths>().Draw(tiles);
	}

	public GridLoc GetRandom(float max)
	{
		List<GridLoc> t = new List<GridLoc>(tiles.Keys);
		return t[Random.Range(0, Mathf.RoundToInt(t.Count * max))];
	}
}
