using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class WindTile : MonoBehaviour {

	public AreaEffector2D af;
	public float tempMagnitude, tempAngle;

	void Awake () {
		af = GetComponent <AreaEffector2D> ();
	}
}
