using UnityEngine;
using System.Collections;

public class Tiler : MonoBehaviour {

	public GameObject tilePrefab;
	public Transform parent;
	public int rows, cols;
	public Vector2 tileSize;
	public bool addBorder;
	public float borderBuffer;
	public Vector3 offset;

	// Use this for initialization
	void Awake () {
		SpriteRenderer tileSpriteRenderer = tilePrefab.GetComponent<SpriteRenderer> ();
		if (tileSpriteRenderer != null && tileSpriteRenderer.sprite != null)
			tileSize = tileSpriteRenderer.sprite.bounds.size;
		else {
			BoxCollider2D tileboxCollider2D = tilePrefab.GetComponent<BoxCollider2D> ();
			if (tileboxCollider2D != null)
				tileSize = tileboxCollider2D.size;
		}
		float maxX = rows * tileSize.x / 2;
		float maxY = rows * tileSize.y / 2;
		for (float i = -maxX; i < maxX; i += tileSize.x)
			for (float j = -maxY; j < maxY; j += tileSize.y) {
				GameObject tile = (GameObject)Instantiate (tilePrefab, new Vector3 (i, j, 0), Quaternion.identity);
				tile.transform.SetParent (parent);
			}
		if (addBorder) {
			EdgeCollider2D border = parent.gameObject.AddComponent<EdgeCollider2D> ();
			border.points = new Vector2[]{new Vector2(-maxX + borderBuffer, -maxY + borderBuffer),
				new Vector2(-maxX + borderBuffer, maxY - borderBuffer),
				new Vector2(maxX - borderBuffer, maxY - borderBuffer),
				new Vector2(maxX - borderBuffer, -maxY + borderBuffer),
				new Vector2(-maxX + borderBuffer, -maxY + borderBuffer)};
		}
		transform.localPosition += offset;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
