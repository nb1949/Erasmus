using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class Wind : MonoBehaviour {

	private AreaEffector2D af;
	[Range(60, 600)]
	public float changeIntervalMin;
	[Range(60, 600)]
	public float changeIntervalMax;

	void Awake () {
		af = GetComponent <AreaEffector2D> ();
		WindShift ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void WindShift() {
		af.forceMagnitude = Random.Range (0.2f, 1);
		af.forceAngle = Random.Range (0, 359);
		af.forceVariation = Random.Range (0, 0.5f);
		Invoke ("WindShift", Random.Range (changeIntervalMin, changeIntervalMax));
	}
}
