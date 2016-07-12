using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class FadeOut : MonoBehaviour {

	private AudioSource a;
	private float fadeOutTime;
	private float startVolume;

	// Use this for initialization
	void Awake () {
		if(a == null)
			a = GetComponent<AudioSource> ();
	}

	public void StartFading(float fadeOutTime) {
		startVolume = a.volume;
		this.fadeOutTime = fadeOutTime;
		InvokeRepeating ("_fade", Time.deltaTime, Time.deltaTime);
	}

	private void _fade() {
		if (a.volume < 0.01)
			CancelInvoke ("_fade");
		else
			a.volume -= startVolume * Time.deltaTime / this.fadeOutTime;
	}

}
