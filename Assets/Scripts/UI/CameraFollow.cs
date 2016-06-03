using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine.SocialPlatforms;

public class CameraFollow : MonoBehaviour {

	public CreaturesStatistics creaturesStatistics;
	public float smoothTime = 0.5f;
	public float zoomSmoothTime = 1f;
	[Range(0,4)]
	public int buffer;
	[Range(6,20)]
	public int maxSize;
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
		if (minSize < radius && radius < maxSize ) {
			cam.orthographicSize = Mathf.SmoothDamp (cam.orthographicSize, radius + buffer,
				ref zoomVelocity, zoomSmoothTime);
		}
	}
}
