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
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ControlSplit(float slowdown) {
		if (splitMode || joinMode) {
			Time.timeScale = 1;
			splitMode = false;	
			joinMode = false;
		} else if(!joinMode)  {
			Time.timeScale = slowdown;
			splitMode = true;
		}
	}

	public void Split(GameObject creature) {
		creature.GetComponent<CreatureReproduction> ().Reproduce ();
		Time.timeScale = 1;
		splitMode = false;
	}

	public void ControlJoin(float slowdown) {
		if (joinMode || splitMode) {
			Time.timeScale = 1;
			joinMode = false;	
			splitMode = false;
		} else if(transform.childCount > 1 && !splitMode) {
			Time.timeScale = slowdown;
			joinMode = true;
		}
	}

	public void Unjoin() {
		firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
		firstToJoin = null;
		Time.timeScale = 1;
		joinMode = false;
	}

	public void Join(GameObject creature) {
		if (firstToJoin == null) {
			firstToJoin = creature;
			firstToJoin.GetComponent<CreatureInteraction> ().selected = true;
	} else {
			firstToJoin.GetComponent<CreatureReproduction> ().Unite (creature);
			firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
			creature.GetComponent<CreatureInteraction> ().selected = false;
			firstToJoin = null;
			Time.timeScale = 1;
			joinMode = false;
		}
	}
}
