using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickRestart : MonoBehaviour {

	public void RestartGame() {
		SceneManager.LoadScene ("main");
	}
}
