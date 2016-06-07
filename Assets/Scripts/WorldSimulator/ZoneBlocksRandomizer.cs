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
	public LayerMask avoid;
	public int avoidOffset;
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
				Vector2 spawnPosition = randomDirection.normalized * minOffset + randomDirection * offset;
				if (!Physics2D.OverlapCircle (spawnPosition, avoidOffset, avoid)) {
					GameObject iGo = (GameObject)Instantiate (
						                prefab.go, 
						                spawnPosition,
						                Quaternion.identity);
					iGo.transform.localScale *= Random.Range (minScale, maxScale);
					iGo.transform.SetParent (parent, true);
				}
				else 
					i--;
			}
		}
	}
}
