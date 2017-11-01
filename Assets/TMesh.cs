using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMesh : MonoBehaviour
{
	public GameObject test;

	public void GenMesh(Texture2D tex)
	{
		for(int x = 0; x < tex.width; x++)
		{
			for (int y = 0; y < tex.height; y++)
			{
				int h = Mathf.RoundToInt(tex.GetPixel(x, y).g * 5f);
				float fZ = y;
				if (x % 2 == 0)
					fZ += 0.5f;
				Instantiate(test, new Vector3(x * 0.86612f, h / 3f, fZ), Quaternion.identity);
			}
		}
	}
}
