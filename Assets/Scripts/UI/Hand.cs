using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour {

	public bool mouseOn;
	public CreaturesSplitJoinController creatureController;
	public GameObject[] fingers;
	public int[] activeFingers;

	// Update is called once per frame
	void Update () {
		if (!creatureController.joinMode && !creatureController.splitMode) {
			if (mouseOn) {
				if (Input.GetMouseButtonDown (0))
					fingers [0].SetActive (true);
				else if (Input.GetMouseButtonUp (0))
					fingers [0].SetActive (false);
				if (fingers [0].activeInHierarchy)
					fingers [0].GetComponent<Finger> ().UpdatePosition (Input.mousePosition);
			} else {
				ClearHand ();
				Touch[] touches = Input.touches;
				for (int i = 0; i < Input.touchCount; i++) {
					int fingerID = touches [i].fingerId; 
					if (fingerID < activeFingers.Length)
						activeFingers [fingerID] = i;
				}
				for (int id = 0; id < activeFingers.Length; id++) {
					if (activeFingers [id] > -1) {
						fingers [id].SetActive (true);
						fingers [id].GetComponent<Finger> ().UpdatePosition (activeFingers [id]);
					} else
						fingers [id].SetActive (false);
				}
			}
		}
	}

	private void ClearHand() {
		for (int i = 0; i < activeFingers.Length; i++) {
			fingers [i].SetActive (false);
			activeFingers [i] = -1;
		}
	}

	void OnDisable() {
		ClearHand ();
	}
}
