using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnOnInfoPanel : MonoBehaviour {

	public GameObject infoPanel;
	public GameObject directionalManager;
	public Text infoPanelText;

	public void TurnOn(string msg) {
		transform.parent.gameObject.SetActive (false);
		directionalManager.SetActive (false);
		infoPanelText.text = msg;
		infoPanel.SetActive (true);
	}
}
