using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public CreaturesStatistics statistics;
	private int ptsPerCreature = 3;
	private Text txt;
	private int score = 0;
	public int updateRate = 1;

	// Use this for initialization
	void Awake(){
		txt = GetComponent<Text> ();
		txt.text = "Score: " + score;
		InvokeRepeating ("UpdateScore", 0, this.updateRate);
	}

	void UpdateScore(){
		score += statistics.count * ptsPerCreature;
		txt.text = "Score: " + score;
	}

}
