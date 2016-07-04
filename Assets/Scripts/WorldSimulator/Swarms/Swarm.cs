using UnityEngine;
using System.Collections;

public class Swarm : MonoBehaviour {

	public GameObject swarmSinglePrefab;
	public int num;
	public float offset;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < num; i++) {
			GameObject swarmSingle = (GameObject)Instantiate (swarmSinglePrefab, transform.position +
				new Vector3(Random.Range (-offset, offset), Random.Range (-offset, offset), 0), Quaternion.identity);
			swarmSingle.transform.parent = transform;
		}
		InvokeRepeating ("JustifyExistence", 1, 1);
	}

	private void JustifyExistence() {
		if(transform.childCount < 1) {
			CancelInvoke ();
			Destroy (this.gameObject);
		}
	}

}
