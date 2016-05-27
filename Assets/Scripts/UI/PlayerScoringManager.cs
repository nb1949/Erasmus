using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerScoringManager : MonoBehaviour {

	public Text UIText;
	public int totalDeaths;
	public int totalGoodDeaths;
	public int totalSplits;
	public int totalJoins;
	public int totalDNA;
	public int DNA;
	public int masterSplitter;
	public int masterJoiner;
	public int splitPrice;
	public int joinPrice;


	// Use this for initialization
	void Awake(){
		UIText.text = "DNA: " + DNA;
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
		UIText.text = "DNA: " + DNA;
	}

	private void CalculateDNAOnSplit() {
		Debug.Log ("Split");
		if (++totalSplits == masterSplitter) 
			Debug.Log ("Achievement: Master Splitter with total of " + totalSplits + " splits!!");
		DNA -= splitPrice;
		UIText.text = "DNA: " + DNA;
	}

	private void CalculateDNAOnJoin() {
		Debug.Log ("Join");
		if (++totalJoins == masterJoiner)
			Debug.Log ("Achievement: Master Joiner with total of " + totalJoins + " joins!!");
		DNA -= joinPrice;
		UIText.text = "DNA: " + DNA;
	}
}
