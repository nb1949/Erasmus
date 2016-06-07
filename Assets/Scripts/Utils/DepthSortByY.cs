using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class DepthSortByY : MonoBehaviour
{

	public float yOffset;
	private const int IsometricRangePerYUnit = 40;

	void Update()
	{
		Renderer renderer = GetComponent<Renderer>();

		renderer.sortingOrder = -(int)((transform.position.y + yOffset) * IsometricRangePerYUnit);
	}
}
