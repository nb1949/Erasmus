using UnityEngine;
using System.Collections;

public class Stationary : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other) {
		ConditionalEffect effect = other.gameObject.AddComponent<ConditionalEffect> ();
		CollidesCondition collidesCond = other.gameObject.AddComponent<CollidesCondition> ();
		collidesCond.one = GetComponent<Collider2D> ();
		collidesCond.other = other;
		effect.condition = collidesCond;
		effect.deltaTime = 4;
		effect.terminationTime = 10;
		effect.Set ("health", 1);
		effect.Apply ();
	}
}
