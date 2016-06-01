using UnityEngine;
using System.Collections;

public class RandomAnimateSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().speed = Random.Range (0.3f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
