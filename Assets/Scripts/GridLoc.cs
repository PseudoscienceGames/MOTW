using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridLoc
{
	public int x;
	public int y;

	public GridLoc(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public GridLoc Move(int dir)
	{
		GridLoc g = new GridLoc(x, y);
		if (dir == 0)
		{
			if (g.y % 2 == 0)
				g.x += 1;
			g.y += 1;
		}
		if (dir == 1)
		{
			g.x += 1;
		}
		if (dir == 2)
		{
			if (g.y % 2 == 0)
				g.x += 1;
			g.y += -1;

		}
		if (dir == 3)
		{
			if (g.y % 2 != 0)
				g.x += -1;
			g.y += -1;

		}
		if (dir == 4)
		{
			g.x += -1;
		}
		if (dir == 5)
		{
			if (g.y % 2 != 0)
				g.x += -1;
			g.y += 1;
		}
		return g;
	}

	public Vector3 WorldSpace()
	{
		float fX = x;
		if (y % 2 == 0)
			fX += 0.5f;
		Vector3 pos = new Vector3(fX, 0, y * 0.86612f);
		return pos;
	}

	public override string ToString()
	{
		return string.Format("({0},{1})", x, y);
	}
}
