using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO.IsolatedStorage;

public class CreaturesSplitJoinController : MonoBehaviour {

	public bool splitMode;
	public bool joinMode;
	public PlayerScoringManager psm;
	public Text infoPanelText;
	public AudioSource audioS;
	private GameObject firstToJoin;

	// Use this for initialization
	void Start () {
		splitMode = false;
		joinMode = false;
	}

	public bool ModeIsOn() {
		return splitMode || joinMode;
	}

	public void ClearModes() {
		splitMode = false;
		joinMode = false;
		if (firstToJoin != null) {
			CreatureInteraction interact = firstToJoin.GetComponent<CreatureInteraction> ();
			interact.selected = false;
			firstToJoin = null;
		}
	}

	public void EnterSplitMode() {
		splitMode = true;
	}

	public bool Split(GameObject creature) {
		if (psm.DNA < psm.splitPrice) {
			Inform ("Not Enough DNA");
			return false;
		}
		ReproductionCode rc = creature.GetComponent<CreatureReproduction> ().Split ();
		switch (rc) {
			case ReproductionCode.OK:
				Inform ("Split Succeeded!");
				return true;
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
		return false;
	}

	public void EnterJoinMode() {
		joinMode = true;
	}

	public void Unjoin() {
		firstToJoin.GetComponent<CreatureInteraction> ().selected = false;
		firstToJoin = null;
	}

	public bool Join(GameObject creature) {
		if (psm.DNA < psm.joinPrice) {
			Inform ("Not Enough DNA");
			return false;
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
						return true;
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
		return false;
	}

	private void Inform(string msg) {
		audioS.Play ();
		StartCoroutine ("DisplayMsg", msg);		
		StartCoroutine ("RemoveMsg");
	}

	private IEnumerator DisplayMsg(string msg) {
		infoPanelText.text = "";
		yield return new WaitForSeconds (0.7f);
		infoPanelText.text = msg;
	}

	private IEnumerator RemoveMsg() {
		yield return new WaitForSeconds (5);
		infoPanelText.text = "";
	}
}
