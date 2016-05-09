using UnityEngine;
using System.Collections;

public abstract class Drop : MonoBehaviour {

	[Range(0,10)]
	public float timeout;
	[Range(0,100)]
	public float value;

	public abstract void Hit (GameObject hit);

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Set(float timeout, float value) {
		this.timeout = timeout;
		this.value = value;
		DestroyObject (gameObject, timeout);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Creature") || other.transform.CompareTag ("Player")) {
			this.Hit(other.gameObject);
			Object.Destroy (gameObject);
		}
	}
}
