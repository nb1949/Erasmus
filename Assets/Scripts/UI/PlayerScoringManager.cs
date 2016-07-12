using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerScoringManager : MonoBehaviour {

	public Text DNAText;
	public Animator AUIAnimator;
	public Animator DUIAnimator;
	public int totalDeaths;
	public int totalGoodDeaths;
	public int totalSplits;
	public int totalJoins;
	public int totalDNA;
	public int DNA;
	public int achievmentAward;
	public int splitterThresh;
	public int joinerThresh;
	public int biologistThresh;
	public int hearthThresh;
	public int splitPrice;
	public int joinPrice;
	public bool showDemoAchievements = false; 
	private int achievementsShown = 0;


	// Use this for initialization
	void Awake(){
		DNAText.text = DNA.ToString ();
		CreatureEvents.OnDeath += CalculateDNAOnDeath;
		CreatureEvents.OnSplit += CalculateDNAOnSplit;
		CreatureEvents.OnJoin += CalculateDNAOnJoin;
		if (showDemoAchievements) {
			Invoke ("_demoAchievement", 9f);
		}

	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.E)) {
			DNA += 100;
			UpdateDNAGUI ();
		}
	}

	private void _demoAchievement(){
		achievementsShown++; 
		if (achievementsShown == 1) {
			AUIAnimator.SetTrigger ("Splitter");
			Invoke ("_demoAchievement", 3f);
		} else if (achievementsShown == 2) {
			AUIAnimator.SetTrigger ("Biologist");
			Invoke ("_demoAchievement", 3f);
		} else if (achievementsShown == 3) {
			AUIAnimator.SetTrigger ("Heart");
			Invoke ("_demoAchievement", 3f);
		} else if (achievementsShown == 4) {
			AUIAnimator.SetTrigger ("Mater");
			Invoke ("_demoAchievement", 3f);
		}
	}

	private void CalculateDNAOnDeath(Creature creature) {
		Debug.Log ("Death");
		totalDeaths++;
		int healthAtTimeOfDeath = (int)creature.props.Get ("health");
		if (healthAtTimeOfDeath > creature.props.health * 0.7f) {
			totalGoodDeaths++;
			totalDNA += healthAtTimeOfDeath;
			DNA += healthAtTimeOfDeath;
		}

		if (totalDNA > biologistThresh) {
			Debug.Log ("Achievement: Biologist with total of " + totalDNA + " DNA accumulated!!");
			AUIAnimator.SetTrigger ("Biologist");
			totalDNA += achievmentAward;
			DNA += achievmentAward;
			biologistThresh = int.MaxValue;
		}
		if (totalGoodDeaths == hearthThresh) {
			Debug.Log ("Achievement: Heart with total of " + totalGoodDeaths + " long living creatures!!");
			AUIAnimator.SetTrigger ("Heart");
			totalDNA += achievmentAward;
			DNA += achievmentAward;
		}
		UpdateDNAGUI ();
	}

	private void CalculateDNAOnSplit() {
		Debug.Log ("Split");
		if (++totalSplits == splitterThresh) {
			Debug.Log ("Achievement: Master Splitter with total of " + totalSplits + " splits!!");
			AUIAnimator.SetTrigger ("Splitter");
			totalDNA += achievmentAward;
			DNA += achievmentAward;
		}
		DNA -= splitPrice;
		UpdateDNAGUI ();
	}

	private void CalculateDNAOnJoin() {
		Debug.Log ("Mate");
		if (++totalJoins == joinerThresh) {
			Debug.Log ("Achievement: Master Mater with total of " + totalJoins + " matings!!");
			AUIAnimator.SetTrigger ("Mater");
			totalDNA += achievmentAward;
			DNA += achievmentAward;
		}
		DNA -= joinPrice;
		UpdateDNAGUI ();
	}

	private void UpdateDNAGUI() {
		DNAText.text = DNA.ToString ();
		if(DUIAnimator.isInitialized)
			DUIAnimator.SetTrigger ("OnChange");
	}
}
