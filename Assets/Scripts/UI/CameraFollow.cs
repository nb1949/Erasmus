using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System;

public class CameraFollow : MonoBehaviour {

	public CreaturesStatistics creaturesStatistics;
	public float smoothTime = 0.5f;
	public float zoomSmoothTime = 1f;
	public int buffer;
	private Camera cam;
	private float minSize;
	private Vector3 velocity = Vector3.zero;
	private float zoomVelocity = 0;

	void Awake() {
		cam = gameObject.GetComponent<Camera> ();
		minSize = cam.orthographicSize;
	}

	void FixedUpdate () {
		Vector3 goalPos = creaturesStatistics.meanPosition;
		goalPos.z = transform.position.z;
		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
		int radius = Mathf.CeilToInt (creaturesStatistics.groupRadius);
		if (radius > minSize) {
			cam.orthographicSize = Mathf.SmoothDamp (cam.orthographicSize, radius + buffer,
				ref zoomVelocity, zoomSmoothTime);
		}
	}
}
