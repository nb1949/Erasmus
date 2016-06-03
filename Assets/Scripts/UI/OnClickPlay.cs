using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickPlay : MonoBehaviour {

	public Image image;
	public Sprite nextSprite;
	public GameObject restart;
	private bool firstTime = true;
	public GameObject game;
	public GameObject gameUI;
	public GameObject menuUI;
	public GameObject zoneChanger;

	public void PlayGame() {
		if (firstTime) {
			image.overrideSprite = nextSprite;
			zoneChanger.SetActive (false);
			restart.SetActive (true);
		}
		game.SetActive (true);
		gameUI.SetActive (true);
		menuUI.SetActive (false);
		Time.timeScale = 1;
	}
}
