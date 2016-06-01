using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickPause : MonoBehaviour {

	public GameObject game;
	public GameObject gameUI;
	public GameObject menuUI;

	public void PauseGame() {
		menuUI.SetActive (true);
		gameUI.SetActive (false);
		Time.timeScale = 0;
	}
}
