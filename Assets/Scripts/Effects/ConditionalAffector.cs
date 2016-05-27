using UnityEngine;
using System.Collections;

public abstract class ConditionalAffector : Affector {

	protected Condition condition;
	public float deltaTime;
	public float terminationTime;

	public override abstract void Affect(GameObject creature);
}
