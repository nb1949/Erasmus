using UnityEngine;
using System.Collections;

public class WaterDrop : Drop {

	public string property;

	public override void Hit (GameObject hit) {
		ConstantEffect effect = hit.AddComponent<ConstantEffect> ();
		effect.Set (property, value);
		effect.Apply ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
