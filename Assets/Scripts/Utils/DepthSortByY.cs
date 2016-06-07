using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class DepthSortByY : MonoBehaviour
{

	private const int IsometricRangePerYUnit = 40;

	void Update()
	{
		Renderer renderer = GetComponent<Renderer>();
		renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
	}
}
