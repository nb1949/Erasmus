using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public CreaturesStatistics stats; 
	public GameObject[] activeGame;
	public GameObject[] GameOverObjects;
	public bool gameStatred = false;
		

	// Use this for initialization
	void Start () {
	
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
	void Update () {
		if (gameStatred && stats.count == 0) {
			foreach (GameObject g in activeGame) {
				g.SetActive (false);
			}
			foreach (GameObject g in GameOverObjects) {
				g.SetActive (true);
			}
		}
	}
}
