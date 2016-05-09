using UnityEngine;
using System.Collections;

public class CreaturesStatistics : MonoBehaviour {

	[Range(1,10)]
	public int samplingRate;
	public int count;
	public float totalMass;
	public Vector2 centerOfMass;



	// Use this for initialization
	void Start () {
		InvokeRepeating ("GetStatistics", 0, samplingRate);	
	}

	// Update is called once per frame
	void Update () {
	}

	private void GetStatistics() {
		count = transform.childCount;
		totalMass = 0;
		centerOfMass = Vector2.zero;
		foreach (Transform creature in transform) {
			int creatureFactor = creature.CompareTag ("Player") ? count : 1;
			Rigidbody2D body = creature.gameObject.GetComponent<Rigidbody2D> ();
			totalMass += creatureFactor * body.mass;
			centerOfMass += body.worldCenterOfMass * creatureFactor *  body.mass;
		}
		centerOfMass /= totalMass;
	}
}
