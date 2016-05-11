using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PausePlay : MonoBehaviour {

	public GameObject textGameObject;
	private Text textComp;

	void Awake() {
		textComp = textGameObject.GetComponent<Text> ();
	}

	public void TogglePausePlay() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			textComp.text = "Pause";
		} else if (Time.timeScale == 1) {
			Time.timeScale = 0;
			textComp.text = "Play";
		}
	}
}
