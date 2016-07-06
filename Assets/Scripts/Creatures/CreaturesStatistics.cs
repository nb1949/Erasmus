using UnityEngine;
using System.Collections;

public class CreaturesStatistics : MonoBehaviour {

	public int count;
	public Vector2 meanPosition;
	public float groupRadius;
	private Creatures creatures;
	private float minXpos;
	private float maxXpos;
	private float minYpos;
	private float maxYpos;

	// Use this for initialization
	void Awake () {
		creatures = GetComponent<Creatures> ();
	}

	// Update is called once per frame
	void Update () {
		count = creatures.pool.activeCount;
		if (count > 0) {
			resetPositions ();
			foreach (Transform creature in transform) {
				if (creature.gameObject.activeInHierarchy) {
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
			}
			meanPosition /= count;
			groupRadius = Mathf.Max (new float[] { 
				Mathf.Abs (meanPosition.x - minXpos), 
				Mathf.Abs (meanPosition.x - maxXpos),
				Mathf.Abs (meanPosition.y - minYpos),
				Mathf.Abs (meanPosition.y - maxYpos)
			});
		} else {
			Debug.Log ("All creatures are dead.");
			gameObject.SetActive (false);
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
