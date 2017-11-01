using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TData : MonoBehaviour
{
	public int size;
	public Texture2D tex;
	// Use this for initialization
	void Start () {
		tex = new Texture2D(size, size);
		for(int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				tex.SetPixel(x, y, new Color(0, Random.Range(0, 5) / 5f, 0, 1));
			}
		}
		tex.filterMode = FilterMode.Point;
		tex.Apply();
		GetComponent<Renderer>().material.mainTexture = tex;
		System.IO.File.WriteAllBytes(Application.dataPath + "/Test.png", tex.EncodeToPNG());
		GetComponent<TMesh>().GenMesh(tex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
