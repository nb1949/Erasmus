using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreatureInteraction : MonoBehaviour {

	public CreaturesController controller;
	public bool selected;
	private TextMesh stats;
	private Creature creature;
	private Quaternion fixedRotation;

	void Awake () {
		selected = false;
		creature = GetComponent <Creature> ();
		stats = transform.FindChild ("Stats").GetComponent<TextMesh> ();
		stats.gameObject.GetComponent <MeshRenderer> ().sortingLayerName = "Text";
		fixedRotation = Quaternion.identity;
	}

	void Update() {
		stats.transform.rotation = fixedRotation;
	}

	void OnMouseEnter() {
		stats.text = "Health: " + creature.stats.getProp("health");

		foreach (Genetics.GeneType gene in Genetics.DNA_PROPERTIES){
			stats.text += ("\n" + gene.ToString () + ": " + creature.stats.genome [gene].Val);
		}
	}

	void OnMouseExit() {
		stats.text = "";
	}


	void OnMouseDown() {
	}

	void OnMouseUp(){
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
