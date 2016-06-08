using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine.SocialPlatforms;

public class CameraFollow : MonoBehaviour {

	public CreaturesStatistics creaturesStatistics;
	public float smoothTime = 0.5f;
	public float zoomSmoothTime = 1f;
	[Range(1,6)]
	public float zoomSize;
	[Range(0,4)]
	public float buffer;
	[Range(6,50)]
	public float maxSize;
	private bool zoomMode;
	private Transform zoomInCreature;
	private Camera cam;
	private float minSize;
	private Vector3 velocity = Vector3.zero;
	private float zoomVelocity = 0;

	void Awake() {
		zoomMode = false;
		cam = gameObject.GetComponent<Camera> ();
		minSize = cam.orthographicSize;
	}

	void FixedUpdate () {
		if (zoomMode)
			FollowCreature (zoomInCreature);
		else
			FollowAll ();
	}

	public void ZoomOn (Transform creature) {
		this.zoomMode = true;
		this.zoomInCreature = creature;
	}

	public bool IsZoomOn() {
		return this.zoomMode;
	}

	public void ZoomOff () {
		this.zoomMode = false;
		this.zoomInCreature = null;
	}

	public bool AmIOnZoom(Transform creature) {
		return zoomMode && zoomInCreature != null && zoomInCreature.Equals (creature);
	}

	private void FollowCreature(Transform creature) {
		Vector3 goalPos = creature.position;
		goalPos.z = transform.position.z;
		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
		cam.orthographicSize = Mathf.SmoothDamp (cam.orthographicSize, zoomSize,
				ref zoomVelocity, zoomSmoothTime);
	}

	private void FollowAll() {
		Vector3 goalPos = creaturesStatistics.meanPosition;
		goalPos.z = transform.position.z;
		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
		int radius = Mathf.CeilToInt (creaturesStatistics.groupRadius);
		if ((minSize < radius && radius < maxSize) || cam.orthographicSize < minSize) {
			cam.orthographicSize = Mathf.SmoothDamp (cam.orthographicSize, radius + buffer,
				ref zoomVelocity, zoomSmoothTime);
		}
	}
}
