using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class WindTile : MonoBehaviour {

	public AreaEffector2D af;
	public GameObject graphics;
	public float tempMagnitude, tempAngle;

	void Awake () {
		if(af == null)
			af = GetComponent <AreaEffector2D> ();
		if (graphics != null && graphics.activeInHierarchy)
			InvokeRepeating ("UpdateWindGraphics", 1, 1);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.W) && graphics != null){
			if(!graphics.activeInHierarchy)  {
				graphics.SetActive (true);
				InvokeRepeating ("UpdateWindGraphics", 1, 1);
			} else {
				graphics.SetActive (false);
				CancelInvoke ("UpdateWindGraphics");
			}
		}
	}

	private void UpdateWindGraphics() {
			graphics.transform.localRotation = Quaternion.AngleAxis(af.forceAngle, Vector3.forward);
			graphics.GetComponent<SpriteRenderer> ().color = 
				new Color(255,255,255, 0.25f + af.forceMagnitude * 0.15f);
	}
}
