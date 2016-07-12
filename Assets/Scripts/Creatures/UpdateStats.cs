using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour {

	private Text stats;
	private Creature creature;

	void Start() {
		stats = GetComponent<Text> ();
		creature = transform.parent.parent.parent.gameObject.GetComponent<Creature> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale > 0)
			stats.text = "HP: " + Mathf.Clamp (creature.props.Get ("health"), 0, creature.props.health) +
				"  | " + _GetStageStr(creature.props);
		else
			stats.text = "";
	}

	private string _GetStageStr(CreatureProperties props) {
		if(props.IsDying ())
			return "Withering";
		else if(props.IsYoung ())
			return "Child";
		else if(props.IsOld ())
			return "Elder";
		else
			return "Adult";
	}
}
