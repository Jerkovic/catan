using System;
using UnityEngine;
using System.Collections;
using logic;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;

	private const float xOffset = 0.882f;
	private const float zOffset = 0.764f;

	// Use this for initialization
	private void Start ()
	{
		var board = new Board(3);
		
		foreach (var tile in board.tiles)
		{			
			var hexGo = Instantiate(hexPrefab, tile.ToWorldCoordinates(), Quaternion.identity);						 
			hexGo.name = tile.getId().ToString();
			var meshRenderer = hexGo.GetComponentInChildren<MeshRenderer>();
			meshRenderer.material.color = tile.color;
			hexGo.transform.SetParent(transform);
			hexGo.isStatic = true;
		}
		
		// test corner placement
		var t1 = board.tiles[0].ToWorldCoordinates();
		var t2 = board.tiles[1].ToWorldCoordinates();
		var t3 = board.tiles[2].ToWorldCoordinates();
		var centerX = (t1.x + t2.x + t3.x) / 3;
		var centerZ = (t1.z + t2.z + t3.z) / 3;
		Instantiate(hexPrefab, new Vector3(centerX, 0.2f, centerZ), Quaternion.identity);		
	}		
}
