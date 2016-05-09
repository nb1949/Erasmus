using UnityEngine;
using System.Collections;

public abstract class Collectible : MonoBehaviour {

	[Range(1, 1000)]
	public int halfLife;
	[Range(0, 100)]
	public float value;

	public abstract void Consume (GameObject collector);

	void Awake() {
		InvokeRepeating ("Decay", 0, 1);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Set(int halfLife, float value) {
		this.halfLife = halfLife;
		this.value = value;
	}

	private void Decay() {
		if (halfLife-- <= 0) {
			CancelInvoke ("Decay");
			Object.Destroy (gameObject);
		}
		
	}
}
