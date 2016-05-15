using UnityEngine;
using System.Collections;

public class Finger : MonoBehaviour {

	public int fingerID;
	
	// Update is called once per frame
	public void UpdatePosition (int index) {
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint (Input.touches[index].position);
	}
}
