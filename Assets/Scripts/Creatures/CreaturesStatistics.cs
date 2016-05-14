using UnityEngine;
using System.Collections;

public class CreaturesStatistics : MonoBehaviour {

	public int count;
	public Vector2 meanPosition;
	public float groupRadius;
	private float minXpos;
	private float maxXpos;
	private float minYpos;
	private float maxYpos;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate () {
		count = transform.childCount;
		if (count > 0) {
			resetPositions ();
			foreach (Transform creature in transform) {
				Vector2 pos = (Vector2)creature.position;
				meanPosition += pos;
				if (pos.x < minXpos)
					minXpos = pos.x;
				if (pos.x > maxXpos)
					maxXpos = pos.x;
				if (pos.y < minYpos)
					minYpos = pos.y;
				if (pos.y < maxYpos)
					maxYpos = pos.y;
			}
			meanPosition /= count;
			groupRadius = Mathf.Max (new float[] { 
				Mathf.Abs (meanPosition.x - minXpos), 
				Mathf.Abs (meanPosition.x - maxXpos),
				Mathf.Abs (meanPosition.y - minYpos),
				Mathf.Abs (meanPosition.y - maxYpos)
			});
		}
	}

	private void resetPositions() {
		minXpos = meanPosition.x;
		maxXpos = meanPosition.x;
		minYpos = meanPosition.y;
		maxYpos = meanPosition.y;
		meanPosition = Vector2.zero;
	}
}
