using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreatureInteraction : MonoBehaviour {

	public CreaturesController controller;
	public bool selected;
	private bool mouseDown = false;
	private TextMesh stats;
	private CreatureGenome genome;
	private Quaternion fixedRotation;

	void Awake () {
		selected = false;
		genome = GetComponent <CreatureGenome> ();
		stats = transform.FindChild ("Stats").GetComponent<TextMesh> ();
		stats.gameObject.GetComponent <MeshRenderer> ().sortingLayerName = "Text";
		fixedRotation = Quaternion.identity;
	}

	void Update() {
		stats.transform.rotation = fixedRotation;
	}

	void OnMouseEnter() {
		stats.text = "Health: " + genome.properties["health"];
		stats.text += ("\nFat: " + genome.getDna ("fat"));
		stats.text += ("\nHeight: " + genome.getDna ("height"));
		stats.text += ("\nSight: " + genome.getDna ("eyes"));
	}

	void OnMouseExit() {
		stats.text = "";
	}


	void OnMouseDown() {
		mouseDown = true;
	}

	void OnMouseUp(){
		mouseDown = false;
		if (controller.splitMode)
			controller.Split (gameObject);
		else if (controller.joinMode) {
			if (selected) {
				controller.Unjoin ();
			} else {
				controller.Join (gameObject);
			}
		}
	}
}
