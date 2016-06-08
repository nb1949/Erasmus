﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerScoringManager : MonoBehaviour {

	public Text DNAText;
	public Animator AUIAnimator;
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


	// Use this for initialization
	void Awake(){
		DNAText.text = DNA.ToString ();
		CreatureEvents.OnDeath += CalculateDNAOnDeath;
		CreatureEvents.OnSplit += CalculateDNAOnSplit;
		CreatureEvents.OnJoin += CalculateDNAOnJoin;
	}

	private void CalculateDNAOnDeath(Creature creature) {
		Debug.Log ("Death");
		totalDeaths++;
		int healthAtTimeOfDeath = (int)creature.props.Get ("health");
		if (healthAtTimeOfDeath > creature.props.health * 0.7f) {
			totalGoodDeaths++;
			totalDNA += healthAtTimeOfDeath;
			DNA += healthAtTimeOfDeath;
		} else {
			int penalty = Mathf.CeilToInt (healthAtTimeOfDeath / creature.props.health * 10);
			int change = penalty > DNA ? 0 : DNA - penalty; 
			totalDNA += change;
			DNA += change;
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
		DNAText.text = DNA.ToString ();

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
		DNAText.text = DNA.ToString ();
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
		DNAText.text = DNA.ToString ();
	}
}
