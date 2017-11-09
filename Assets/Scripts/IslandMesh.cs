using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class IslandMesh : MonoBehaviour
{
	private List<Vector3> verts = new List<Vector3>();
	private List<int> tris = new List<int>();
	private List<Vector2> uvs = new List<Vector2>();
	public Dictionary<GridLoc, int> points = new Dictionary<GridLoc, int>();
	public float hexSize;
	public float tileHeight;

	public void GenMesh(Dictionary<GridLoc, int> tiles)
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		foreach(KeyValuePair<GridLoc, int> t in tiles)
		{
			Vector3 pos = t.Key.WorldSpace();
			float h = t.Value * tileHeight;
			pos.y += h;
			AddTop(t.Key, pos);
		}
		foreach (KeyValuePair<GridLoc, int> t in tiles)
		{
			AddSides(t.Key);
			AddFillerTris(t.Key);
			
		}
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
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

		uvs.Add(new Vector2(0.5f, 0));
		uvs.Add(new Vector2(1f, .25f));
		uvs.Add(new Vector2(1f, .75f));
		uvs.Add(new Vector2(0.5f, 1));
		uvs.Add(new Vector2(0f, .75f));
		uvs.Add(new Vector2(0f, .25f));


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
			GridLoc g1 = g.Move(i);
			if (points.ContainsKey(g.Move(i)))
			{
				verts.Add(verts[points[g] + i]);
				tris.Add(verts.Count - 1);
				uvs.Add(Vector2.zero);
				int j = i + 4;
				if (j > 5)
					j -= 6;
				verts.Add(verts[(points[g.Move(i)] + j)]);
				tris.Add(verts.Count - 1);
				uvs.Add(new Vector2(0, 1));
				j = i + 1;
				if (j > 5)
					j -= 6;
				verts.Add(verts[points[g] + j]);
				tris.Add(verts.Count - 1);
				uvs.Add(new Vector2(1, 0));
			}
		}
	}
	void AddFillerTris(GridLoc g)
	{
		if (points.ContainsKey(g.Move(0)) && points.ContainsKey(g.Move(1)))
		{
			verts.Add(verts[points[g] + 1]);
			tris.Add(verts.Count - 1);
			uvs.Add(Vector2.zero);
			verts.Add(verts[points[g.Move(0)] + 3]);
			tris.Add(verts.Count - 1);
			uvs.Add(Vector2.zero);
			verts.Add(verts[points[g.Move(1)] + 5]);
			tris.Add(verts.Count - 1);
			uvs.Add(Vector2.zero);
		}
		if (points.ContainsKey(g.Move(5)) && points.ContainsKey(g.Move(0)))
		{
			verts.Add(verts[points[g]]);
			tris.Add(verts.Count - 1);
			uvs.Add(Vector2.zero);
			verts.Add(verts[points[g.Move(5)] + 2]);
			tris.Add(verts.Count - 1);
			uvs.Add(Vector2.zero);
			verts.Add(verts[points[g.Move(0)] + 4]);
			tris.Add(verts.Count - 1);
			uvs.Add(Vector2.zero);
		}
	}
}
