using UnityEngine;
using System.Collections;

// Destroy the gameObject or component after a timer
public class DestroyAfterTime : MonoBehaviour {

	public float timer;

	void Start(){
		// Destroy works with GameObjects and Components
		Destroy(gameObject, timer);
	}
}