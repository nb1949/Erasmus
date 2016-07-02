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
	public GameObject[,] tileMap;

	// Use this for initialization
	void Awake () {
		tileMap = new GameObject[rows, cols];
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
		float x, y;
		int i, j;
		for (i = 0, x = -maxX; i < rows; i++, x += tileSize.x)
			for (j = 0, y = -maxY; j < cols; j++, y += tileSize.y) {
				GameObject tile = (GameObject)Instantiate (tilePrefab, new Vector3 (x, y, 0), Quaternion.identity);
				tile.transform.SetParent (parent);
				tileMap [i, j] = tile;
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
