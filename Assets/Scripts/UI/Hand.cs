using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour {

	public GameObject[] fingers;
	public int[] activeFingers;

	// Update is called once per frame
	void Update () {
		ClearHand ();
		Touch[] touches = Input.touches;
		for(int i = 0; i < Input.touchCount; i++) {
			int fingerID = touches [i].fingerId; 
			if(fingerID < activeFingers.Length)
				activeFingers [fingerID] = i;
		}
		for (int id = 0; id < activeFingers.Length; id++) {
			if (activeFingers [id] > -1 ) {
				fingers [id].SetActive (true);
				fingers [id].GetComponent<Finger> ().UpdatePosition (activeFingers [id]);
			} else 
				fingers [id].SetActive (false);
		}
	}

	private void ClearHand() {
		for (int i= 0; i < activeFingers.Length; i++)
			activeFingers[i] = -1;
	}
}
