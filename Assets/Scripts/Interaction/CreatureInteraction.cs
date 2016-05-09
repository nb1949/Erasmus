using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreatureInteraction : MonoBehaviour {

	private float doubleClickStart = 0;
	private bool mouseDown = false;
	private GameObject mate;
	private TextMesh stats;
	private CreatureReproduction reproduction;
	private CreatureGenome genome;
	private Quaternion fixedRotation;

	void Awake () {
		reproduction = GetComponent <CreatureReproduction> ();
		genome = GetComponent <CreatureGenome> ();
		stats = transform.FindChild ("Stats").GetComponent<TextMesh> ();
		stats.gameObject.GetComponent <MeshRenderer> ().sortingLayerName = "Text";
		fixedRotation = Quaternion.identity;
	}

	void Update() {
		stats.transform.rotation = fixedRotation;
		stats.text = "Health: " + genome.properties["health"];
		stats.text += ("\nFat: " + genome.getDna ("fat"));
		stats.text += ("\nHeight: " + genome.getDna ("height"));
		stats.text += ("\nSight: " + genome.getDna ("eyes"));
	}

	void OnMouseDrag() {
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	void OnMouseOver() {
	
	}


	void OnMouseDown() {
		mouseDown = true;
		Camera.main.GetComponent<FollowPlayer> ().ChangePlayerTo (this.gameObject);
	}

	void OnMouseUp()
	{
		mouseDown = false;
		if ((Time.time - doubleClickStart) < 0.3f) {
			this.OnDoubleClick();
			doubleClickStart = -1;
		} else
			doubleClickStart = Time.time;
		if(this.mate != null && !GameObject.Equals(this.mate , default(GameObject)))
			reproduction.Unite(this.mate);
	}


	void OnDoubleClick()
	{
		reproduction.Reproduce ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (mouseDown && collision.gameObject.CompareTag ("Creature"))
			this.mate = collision.gameObject;
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (mouseDown && collision.gameObject.CompareTag ("Creature"))
			this.mate = default(GameObject);
	}
		
}
