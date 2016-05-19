using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DNAPoints : MonoBehaviour {

	//TODO: Event based currency

	private Text txt;
	private int dnaPoints = 0;

	// Use this for initialization
	void Awake(){
		txt = GetComponent<Text> ();
		txt.text = "DNA: " + dnaPoints;
	}

	void Update(){
	}

}
