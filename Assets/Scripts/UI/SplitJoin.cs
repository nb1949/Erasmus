using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplitJoin : MonoBehaviour {

	public bool splitMode = false;
	public bool joinMode = false;
	public Image splitImage;
	public Image joinImage;

	public void SplitOnOff() {
		if (joinMode) {
			joinMode = false;
			joinImage.color = Color.white;
		}
		splitMode = !splitMode;
		if(splitMode)
			splitImage.color = Color.grey;
		else
			splitImage.color = Color.white;
	}

	public void JoinOnOff() {
		if (splitMode) {
			splitMode = false;
			splitImage.color = Color.white;
		}
		joinMode = !joinMode;
		if(joinMode)
			joinImage.color = Color.grey;
		else
			joinImage.color = Color.white;
	}
		
}
