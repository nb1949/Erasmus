using UnityEngine;
using System.Collections;

public class CreatureSight : MonoBehaviour
{
	
	[Range (0, 359)]
	public float sightAngle;
	[Range (0, 30)]
	public float sightDistance;
	[Range (0, 30)]
	public int sightDensity;
	public LayerMask sightlayerMask;
	private RaycastHit2D[] hits;
	private float angleStep;

	void Awake ()
	{
		hits = new RaycastHit2D[sightDensity];
		angleStep = 2f * sightAngle / sightDensity;
	}

	void FixedUpdate ()
	{
		for (int i = 0; i < sightDensity; i++) {
			Vector3 direction = Quaternion.AngleAxis (-sightAngle + (i - 1) * angleStep, Vector3.forward) * transform.up;
			hits [i] = Physics2D.Raycast (transform.localPosition, direction, sightDistance, sightlayerMask);
			Debug.DrawRay (transform.localPosition, transform.up + direction * sightDistance, Color.red);
		}
	}

	public RaycastHit2D GetSeen (string tag, float dist)
	{
		for (int i = 0; i < sightDensity; i++) {
			if (!RaycastHit2D.Equals (hits [i], default(RaycastHit2D)) &&
				hits[i].transform != null && hits [i].transform.CompareTag (tag) && hits [i].distance < dist)
				return hits [i];
		}
		return default(RaycastHit2D);
	}


	public bool Seen (string tag, float dist)
	{
		for (int i = 0; i < sightDensity; i++) {
			if ((!RaycastHit2D.Equals (hits [i], default(RaycastHit2D)) &&
			    hits [i].transform != null &&
			    hits [i].transform.CompareTag (tag)) && hits [i].distance < dist)
				return true;
		}
		return false;
	}

	public bool Seen (string tag)
	{
		return Seen (tag, sightDistance);
	}
}