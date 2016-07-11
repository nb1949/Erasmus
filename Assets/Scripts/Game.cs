using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public CreaturesStatistics stats; 
	public GameObject[] activeGame;
	public GameObject[] GameOverObjects;
	public bool gameStatred = false;

	void Start() {
		InvokeRepeating ("CheckStatus", 3, 3);
	}

	public void gameStarted(){
		Invoke ("_gameStarted", 3f);
	}
	public void gamePaused(){
		gameStatred = false;
	}
	private void _gameStarted(){
		gameStatred = true;
	}


	// Update is called once per frame
	void CheckStatus () {
		if (gameStatred && stats.count < 1) {
			foreach (GameObject g in activeGame) {
				FadeOut fo = g.GetComponent<FadeOut> ();
				if (fo != null)
					fo.StartFading (6);
			}
			Invoke ("DisableGuis", 10);
			Invoke ("EnableGuis", 0);
		}
	}

	private void  DisableGuis() {
		foreach (GameObject g in activeGame) {
			g.SetActive (false);
		}
	}

	private void EnableGuis() {
		foreach (GameObject g in GameOverObjects) {
			g.SetActive (true);
		}
	}
}
