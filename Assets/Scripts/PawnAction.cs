using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAction : MonoBehaviour
{
	public string actionName;
	public GameObject widget;

	public void SpawnWidget()
	{
		Instantiate(widget, transform.position, Quaternion.identity);
	}
}
