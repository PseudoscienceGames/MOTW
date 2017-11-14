using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPaths : MonoBehaviour
{
	public void Draw(Dictionary<GridLoc, int> tiles)
	{
		foreach(GridLoc g in tiles.Keys)
		{
			for (int i = 0; i <= 5; i++)
			{
				if (tiles.ContainsKey(g.Move(i)))
				{
					if (Mathf.Abs(tiles[g] - tiles[g.Move(i)]) == 1)
					{
						Color c = Color.red;
						Vector3 p1 = g.ToWorld() + (Vector3.up * tiles[g] * .25f);
						Vector3 p2 = g.Move(i).ToWorld() + (Vector3.up * tiles[g.Move(i)] * .25f);
						Debug.DrawLine(p1, p2, Color.red, 1000);
					}
					if (Mathf.Abs(tiles[g] - tiles[g.Move(i)]) == 2)
					{
						Color c = Color.red;
						Vector3 p1 = g.ToWorld() + (Vector3.up * tiles[g] * .25f);
						Vector3 p2 = g.Move(i).ToWorld() + (Vector3.up * tiles[g.Move(i)] * .25f);
						Debug.DrawLine(p1, p2, Color.blue, 1000);
					}
					if (Mathf.Abs(tiles[g] - tiles[g.Move(i)]) == 3)
					{
						Color c = Color.red;
						Vector3 p1 = g.ToWorld() + (Vector3.up * tiles[g] * .25f);
						Vector3 p2 = g.Move(i).ToWorld() + (Vector3.up * tiles[g.Move(i)] * .25f);
						Debug.DrawLine(p1, p2, Color.green, 1000);
					}
				}
			}
		}
	}
}
