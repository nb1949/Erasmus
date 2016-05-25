using UnityEngine;
using System.Collections;

public class Stationary : MonoBehaviour {
	private Affector affector;

	// Use this for initialization
	void Start () {
		Affector affector = this.GetComponent<Affector> ();
		affector.type = AffectorType.WHILE_IN_RANGE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		affector.Affect (other.gameObject);
	}
} 	