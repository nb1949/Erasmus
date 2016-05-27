using UnityEngine;
using System.Collections;

public class Stationary : MonoBehaviour {

	private Affector affector;

	// Use this for initialization
	void Start () {
		affector = this.GetComponent<Affector> ();
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		affector.Affect (other.gameObject);
	}
} 	