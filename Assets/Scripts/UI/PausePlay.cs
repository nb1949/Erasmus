using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PausePlay : MonoBehaviour {

	public Image image;
	public Sprite pauseSprite;
	public Sprite playSprite;

	void Awake() {
	}

	public void TogglePausePlay() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			image.overrideSprite = pauseSprite;
		} else if (Time.timeScale == 1) {
			Time.timeScale = 0;
			image.overrideSprite = playSprite;
		}
	}
}
