using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickPlay : MonoBehaviour {

	public Image image;
	public Sprite nextSprite;
	private bool firstTime = true;
	public GameObject game;
	public GameObject gameUI;
	public GameObject menuUI;

	public void PlayGame() {
		if (firstTime) 
			image.overrideSprite = nextSprite;
		game.SetActive (true);
		gameUI.SetActive (true);
		menuUI.SetActive (false);
		Time.timeScale = 1;
	}
}
