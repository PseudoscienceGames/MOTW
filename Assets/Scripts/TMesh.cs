using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TMesh : MonoBehaviour
{
	private List<Vector3> verts = new List<Vector3>();
	private List<int> tris = new List<int>();
	public Dictionary<GridLoc, int> points = new Dictionary<GridLoc, int>();
	public float hexSize;

	public void GenMesh(Texture2D tex)
	{
		for(int x = 0; x < tex.width; x++)
		{
			for (int y = 0; y < tex.height; y++)
			{
				int h = Mathf.RoundToInt(tex.GetPixel(x, y).g * 5f);
				float fX = x;
				if (y % 2 == 0)
					fX += 0.5f;
				Vector3 pos = new Vector3(fX, h / 3f, y * 0.86612f);
				AddTop(new GridLoc(x, y), pos);
			}
		}
		for (int x = 0; x < tex.width; x++)
		{
			for (int y = 0; y < tex.height; y++)
			{
				AddSides(new GridLoc(x, y));
				AddFillerTris(new GridLoc(x, y));
			}
		}
		GetComponent<MeshFilter>().mesh.vertices = verts.ToArray();
		GetComponent<MeshFilter>().mesh.triangles = tris.ToArray();
		GetComponent<MeshFilter>().mesh.RecalculateNormals();
	}

	void AddTop(GridLoc g, Vector3 pos)
	{
		int count = verts.Count;

		verts.Add(pos + (new Vector3(0, 0, 0.5774f) * hexSize));
		verts.Add(pos + (new Vector3(0.5f, 0, 0.2887f) * hexSize));
		verts.Add(pos + (new Vector3(0.5f, 0, -0.2887f) * hexSize));
		verts.Add(pos + (new Vector3(0, 0, -0.5774f) * hexSize));
		verts.Add(pos + (new Vector3(-0.5f, 0, -0.2887f) * hexSize));
		verts.Add(pos + (new Vector3(-0.5f, 0, 0.2887f) * hexSize));

		points.Add(g, count);

		tris.Add(count);
		tris.Add(count + 1);
		tris.Add(count + 2);

		tris.Add(count);
		tris.Add(count + 2);
		tris.Add(count + 3);

		tris.Add(count);
		tris.Add(count + 3);
		tris.Add(count + 4);

		tris.Add(count);
		tris.Add(count + 4);
		tris.Add(count + 5);
	}
	void AddSides(GridLoc g)
	{
		for (int i = 0; i < 6; i++)
		{
			if (points.ContainsKey(g.Move(i)))
			{
				tris.Add(points[g] + i);
				int j = i + 4;
				if (j > 5)
					j -= 6;
				tris.Add(points[g.Move(i)] + j);
				Debug.Log(points[g.Move(i)]);
				j = i + 1;
				if (j > 5)
					j -= 6;
				tris.Add(points[g] + j);
			}
		}
	}
	void AddFillerTris(GridLoc g)
	{
		if(points.ContainsKey(g.Move(0)) && points.ContainsKey(g.Move(1)))
		{
			tris.Add(points[g] + 1);
			tris.Add(points[g.Move(0)] + 3);
			tris.Add(points[g.Move(1)] + 5);
		}
		if (points.ContainsKey(g.Move(5)) && points.ContainsKey(g.Move(0)))
		{
			tris.Add(points[g]);
			tris.Add(points[g.Move(5)] + 2);
			tris.Add(points[g.Move(0)] + 4);
		}
	}
}

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
		if(dir == 0)
		{
			if (g.y % 2 == 0)
				g.x += 1;
			g.y += 1;
			Debug.Log(x + " " + y + " " + dir + " " + g);
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
		//Debug.Log(x + " " + y + " " + dir + " " + g);
		return g;
	}
	public override string ToString()
	{
		return string.Format("({0},{1})", x, y);
	}
}
