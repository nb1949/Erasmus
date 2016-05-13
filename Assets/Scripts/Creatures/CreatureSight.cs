using UnityEngine;
using System.Collections.Generic;
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
	private ArrayList hits;
	private ArrayList misses;
	private float angleStep;

	void Awake ()
	{
		hits = new ArrayList(sightDensity);
		misses = new ArrayList(sightDensity);
		angleStep = 2f * sightAngle / sightDensity;
	}

	void FixedUpdate ()
	{
		hits.Clear ();
		misses.Clear ();
		for (int i = 0; i < sightDensity; i++) {
			Vector3 direction = Quaternion.AngleAxis (-sightAngle + (i - 1) * angleStep, Vector3.forward) * transform.up;
			RaycastHit2D hit = Physics2D.Raycast (transform.localPosition, direction, sightDistance, sightlayerMask);
			if (hit != default(RaycastHit2D))
				hits.Add (hit.transform);
			else
				misses.Add (direction.normalized);
			Debug.DrawRay (transform.localPosition, transform.up + direction * sightDistance, Color.red);
		}
	}

	public Transform Seen (List<string> tags, float dist) {
		foreach (Transform t in hits)
			if (tags.Contains (t.tag))
				return t;
		return null;
	}

	public Transform Seen (string tag, float dist) {
		foreach (Transform t in hits)
			if (t.CompareTag (tag))
				return t;
		return null;
	}

	public ArrayList GetMisses() {
		return misses;
	}
}