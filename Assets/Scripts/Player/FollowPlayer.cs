using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;

public class FollowPlayer : MonoBehaviour {

	public GameObject currentPlayer;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;

	void Start () {
		this.currentPlayer.GetComponent<CreatureMovement> ().enabled = false;
		this.currentPlayer.GetComponent<PlayerMovement> ().enabled = true;
	}

	void FixedUpdate () {
		Vector3 goalPos = currentPlayer.transform.position;
		goalPos.z = transform.position.z;
		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
	}

	public void ChangePlayerTo(GameObject chosen) {
		this.currentPlayer.GetComponent<PlayerMovement> ().enabled = false;
		this.currentPlayer.GetComponent<CreatureMovement> ().enabled = true;
		this.currentPlayer.tag = "Creature";
		this.currentPlayer = chosen;
		this.currentPlayer.GetComponent<PlayerMovement> ().enabled = true;
		this.currentPlayer.GetComponent<CreatureMovement> ().enabled = false;
		this.currentPlayer.tag = "Player";
	}
}