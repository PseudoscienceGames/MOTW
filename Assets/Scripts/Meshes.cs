using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshes : MonoBehaviour
{
	public GameObject chunk;

	public void GenMeshes(Texture2D tex)
	{
		int size = GetComponent<TData>().size / 32;
		for(int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				GameObject c = Instantiate(chunk) as GameObject;
				GetComponent<TMesh>().GenMesh(tex, new GridLoc(x * 32, y * 32), new GridLoc((x + 1) * 32, (y + 1) * 32), c.GetComponent<MeshFilter>().mesh);
			}
		}
	}
}
