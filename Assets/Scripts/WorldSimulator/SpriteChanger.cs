using UnityEngine;
using System.Collections;

public class SpriteChanger : MonoBehaviour {

	public Sprite sprite;
	// Use this for initialization
	void Awake () {
		foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer> ()) 
			r.sprite = sprite;
	}
}
