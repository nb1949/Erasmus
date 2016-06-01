using UnityEngine;
using System.Collections;

public class ZoneController : MonoBehaviour {

	public CreaturesStatistics creaturesStatistics;
	public Collider2D zoneCollider;
	public GameObject spawners;

	void OnTriggerEnter2D() {
		if (zoneCollider.bounds.Contains (creaturesStatistics.meanPosition)) {
			Debug.Log ("Starting Zone" + gameObject);
			spawners.SetActive (true);
		}
	}

	void OnTriggerExit2D() {
		if (!zoneCollider.bounds.Contains (creaturesStatistics.meanPosition)) {
			Debug.Log ("Closing Zone" + gameObject);
			spawners.SetActive (false);
		}
	}
}
