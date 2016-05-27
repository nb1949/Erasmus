using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreaturesController : MonoBehaviour {

	public bool splitMode;
	public bool joinMode;
	private GameObject firstToJoin;

	// Use this for initialization
	void Start () {
		splitMode = false;
		joinMode = false;
	}

	public void ControlSplit() {
		if (splitMode || joinMode) {
			splitMode = false;	
			joinMode = false;
		} else if(!joinMode)  {
			splitMode = true;
		}
	}

	public void Split(GameObject creature) {
		if (creature.GetComponent<CreatureReproduction> ().Split ()) {
		} else 
			Debug.Log ("Creature is a bit too weak, too young or too old to split");
	}

	public void ControlJoin() {
		if (joinMode || splitMode) {
			joinMode = false;	
			splitMode = false;
		} else if(transform.childCount > 1 && !splitMode) {
			joinMode = true;
		}
	}

	public void Unjoin() {
		firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
		firstToJoin = null;
		joinMode = false;
	}

	public void Join(GameObject creature) {
		if (firstToJoin == null) {
			firstToJoin = creature;
			firstToJoin.GetComponent<CreatureInteraction> ().selected = true;
	} else {
			if (firstToJoin.GetComponent<CreatureReproduction> ().Mate (creature.GetComponent<Creature> ())) {
				firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
				creature.GetComponent<CreatureInteraction> ().selected = false;
				firstToJoin = null;
			} else
				Debug.Log ("Creature is a bit too weak, too young or too old to split");
		}
	}
}
