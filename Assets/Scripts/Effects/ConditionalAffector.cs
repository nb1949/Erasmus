using UnityEngine;
using System.Collections;

public abstract class ConditionalAffector : Affector {

	protected Condition condition;
	public float deltaTime;
	public float terminationTime;

	protected override void _Affect(GameObject creature){
	}
}
