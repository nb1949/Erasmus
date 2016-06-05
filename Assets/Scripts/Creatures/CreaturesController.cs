using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO.IsolatedStorage;

public class CreaturesController : MonoBehaviour {

	public bool splitMode;
	public bool joinMode;
	public PlayerScoringManager psm;
	public Text infoPanelText;
	private GameObject firstToJoin;

	// Use this for initialization
	void Start () {
		splitMode = false;
		joinMode = false;
	}

	public void ClearModes() {
		splitMode = false;
		joinMode = false;
		CreatureInteraction interact = firstToJoin.GetComponent<CreatureInteraction> ();
		interact.selected = false;
		firstToJoin = null;
	}

	public void EnterSplitMode() {
		splitMode = true;
	}

	public void Split(GameObject creature) {
		if (psm.DNA < psm.splitPrice) {
			infoPanelText.text = "Not Enough DNA";
			return;
		}
		ReproductionCode rc = creature.GetComponent<CreatureReproduction> ().Split ();
		switch (rc) {
			case ReproductionCode.OK:
				Inform ("Split Succeeded!");
				break;
			case ReproductionCode.HEALTH:
				Inform ("Creature is too weak to split..");
				break;
			case ReproductionCode.YOUNG:
				Inform ("Creature is too young to split..");
				break;
			case ReproductionCode.OLD:
				Inform ("Creature is too old to split..");
				break;
		}
	}

	public void EnterJoinMode() {
		joinMode = true;
	}

	public void Unjoin() {
		firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
		firstToJoin = null;
	}

	public void Join(GameObject creature) {
		if (psm.DNA < psm.joinPrice) {
			infoPanelText.text = "Not Enough DNA";
			return;
		}
		if (firstToJoin == null) {
			firstToJoin = creature;
			firstToJoin.GetComponent<CreatureInteraction> ().selected = true;
	} else {
			ReproductionCode rc = firstToJoin.GetComponent<CreatureReproduction> ().Mate (creature.GetComponent<Creature> ());
			firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
			creature.GetComponent<CreatureInteraction> ().selected = false;
			firstToJoin = null;
			switch (rc) {
				case ReproductionCode.OK:
					Inform ("Join Succeeded!");
					break;
				case ReproductionCode.HEALTH:
					Inform ("One or both creatures are too weak to join..");
					break;
				case ReproductionCode.YOUNG:
					Inform ("One or both creatures are too young to join..");
					break;
				case ReproductionCode.OLD:
					Inform ("One or both creatures are too old to join..");
					break;
			}
		}
	}

	private void Inform(string msg) {
		infoPanelText.text = msg;
	}
}
