using UnityEngine;
using System.Collections;

public class CollidesCondition : Condition {

	public Collider2D one;
	public Collider2D other;

	public CollidesCondition(Collider2D one, Collider2D other) {
		this.one = one;
		this.other = other;
	}

	public override bool Evaluate() {
		return this.one.IsTouching (other);
	}

}
