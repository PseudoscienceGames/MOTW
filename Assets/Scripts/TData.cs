using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TData : MonoBehaviour
{
	public int size;
	public float frequency;
	public Texture2D tex;
	// Use this for initialization
	void Start () {
		tex = new Texture2D(size, size);
		Dictionary<GridLoc, int> block = new Dictionary<GridLoc, int>();
		
		for(int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				float g = Mathf.PerlinNoise(x / (size / frequency), y / (size / frequency));
				float dX = ((size / 2f) - x) * ((size / 2f) - x);
				float dY = ((size / 2f) - y) * ((size / 2f) - y);
				float dC = Mathf.Sqrt(dX + dY);
				dC = size / 2 - dC;
				dC /= size;

				//Debug.Log(g + " " + dC);
				g *= dC;
				tex.SetPixel(x, y, new Color(0, g, 0, 1));
			}
		}
		tex.filterMode = FilterMode.Point;
		tex.Apply();
		System.IO.File.WriteAllBytes(Application.dataPath + "/Test.png", tex.EncodeToPNG());
		GetComponent<Meshes>().GenMeshes(tex);
	}
}
