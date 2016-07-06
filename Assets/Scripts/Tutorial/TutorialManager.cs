using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {

	private int _tutorialSteps;
	private int _currStep;

	// Use this for initialization
	void Start () {
		Component[] steps = this.GetComponentsInChildren<TutorialStep> (true);
		_currStep = 0;
		_tutorialSteps = steps.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextStep(){
		if (_currStep == _tutorialSteps) {
			return;
		} else if(_currStep == _tutorialSteps-1){
			Component[] steps = this.GetComponentsInChildren<TutorialStep> (true);
			steps [_currStep].gameObject.SetActive (false);
			_currStep++;
		} else {
			Component[] steps = this.GetComponentsInChildren<TutorialStep> (true);
			steps [_currStep].gameObject.SetActive (false);
			_currStep++;
			steps [_currStep].gameObject.SetActive (true);
		}
	}
}
