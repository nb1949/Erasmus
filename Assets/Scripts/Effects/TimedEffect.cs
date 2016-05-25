using UnityEngine;
using System.Collections;

public class TimedEffect : Effect {
		
	[Range(1, 1000)]
	public float terminationTime;

	protected override void ApplyEffect (){
		creature.properties [this.property] -= this.value;
		Invoke ("Reverse", this.terminationTime);
	}

	private void Reverse() {
		creature.properties [this.property] += this.value;
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
