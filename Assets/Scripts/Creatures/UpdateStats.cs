using UnityEngine;
using System.Collections;

public class UpdateStats : MonoBehaviour {

	private TextMesh stats;
	private Creature creature;

	void Start() {
		stats = GetComponent<TextMesh> ();
		creature = transform.parent.parent.gameObject.GetComponent<Creature> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale > 0)
			stats.text = "Health: " + Mathf.Clamp (creature.props.Get ("health"), 0, creature.props.health) +
			"\tAge: " + creature.props.Get ("age");
		else
			stats.text = "";
	}
}
