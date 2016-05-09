using UnityEngine;
using System.Collections;

public class TimedEffect : Effect {

	[Range(1, 1000)]
	public float terminationTime;

	public override void Apply (){
		genome = GetComponent<CreatureGenome> ();
		genome.properties [this.property] += this.value;
		Invoke ("Reverse", this.terminationTime);
	}

	private void Reverse() {
		genome.properties [this.property] -= this.value;
		CancelInvoke ("Reverse");
		Destroy (this);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
