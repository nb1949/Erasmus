using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using AssemblyCSharp;

public class CreatureInteraction : MonoBehaviour {

	public CreaturesController controller;
	public bool selected;
	private bool mouseDown = false;
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
		stats.text = "Health: " + creature.getProp("health");

		foreach (Genetics.GeneType gene in Genetics.DNA_PROPERTIES){
			stats.text += ("\n" + gene.ToString () + ": " + creature.genome [gene].Val);
		}
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
