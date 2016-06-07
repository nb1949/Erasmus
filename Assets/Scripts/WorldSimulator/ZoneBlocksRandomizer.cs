using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameObjectCount {
	public GameObject go;
	public int count;
}

public class ZoneBlocksRandomizer : MonoBehaviour {

	public List<GameObjectCount> prefabs;
	public float minOffset;
	public float offset;
	public Transform parent;
	public float minScale;
	public float maxScale;

	// Use this for initialization
	void Awake () {
		foreach (GameObjectCount prefab in prefabs) {
			for (int i = 0; i < prefab.count; i++) {
				Vector2 randomDirection = Random.insideUnitCircle;
				GameObject iGo = (GameObject)Instantiate (
					prefab.go, 
					randomDirection.normalized * minOffset + randomDirection * offset,
					Quaternion.identity);
				iGo.transform.localScale *= Random.Range (minScale, maxScale);
				iGo.transform.SetParent (parent, true);
			}
			
		}
	}
}
