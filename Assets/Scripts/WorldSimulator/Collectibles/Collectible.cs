using UnityEngine;
using System.Collections;
using System.Resources;

public abstract class Collectible : MonoBehaviour {

	[Range(1, 1000)]
	public int halfLife;
	public int currentLife;
	[Range(0, 100)]
	public float value;

	public abstract void Consume (GameObject collector);

	void Start() {
		InvokeRepeating ("Decay", 0, 1);
	}
	public void Set(int halfLife, float value) {
		this.halfLife = halfLife;
		this.currentLife = this.halfLife;
		this.value = -value;
	}

	private void Decay() {
		if (currentLife-- <= 0) {
			CancelInvoke ();
			gameObject.SetActive (false);
		}
		
	}
}
