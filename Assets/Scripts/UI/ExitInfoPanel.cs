using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitInfoPanel : MonoBehaviour {

	public GameObject ExitTo;
	public Text infoPanelText;

	public void Exit() {
		transform.parent.gameObject.SetActive (false);
		infoPanelText.text = "";
		ExitTo.SetActive (true);
	}

	void Update() {
		if (Input.GetKey (KeyCode.Escape))
			gameObject.GetComponent<Button> ().onClick.Invoke ();
	}
}
