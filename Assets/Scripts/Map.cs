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
		var board = new Board(4);
		
		foreach (var tile in board.tiles)
		{
			var coordinate = tile.coordinate;
			var xPos = coordinate._x * xOffset;
			var zPos = coordinate._z * zOffset;
			
			if( coordinate._z > 0 && coordinate._z % 2 == 1)
			{
				xPos += xOffset / 2f + xOffset * (coordinate._z / 2);
			}
			if( coordinate._z > 0 && coordinate._z % 2 == 0)
			{
				xPos += xOffset * (coordinate._z / 2);
			}
			
			if( coordinate._z < 0 &&  (coordinate._z * -1) % 2 == 1)
			{
				xPos -= (xOffset / 2f) + xOffset * (-coordinate._z / 2);
			}
			
			if( coordinate._z < 0 && (coordinate._z * -1) % 2 == 0) // even 
			{
				xPos -= xOffset * (-coordinate._z / 2);
			}
			
			var hexGo = Instantiate(hexPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);						 
			hexGo.name = "Hex__" + tile.getId();;
			var meshRenderer = hexGo.GetComponentInChildren<MeshRenderer>();
			meshRenderer.material.color = tile.color;
			hexGo.transform.SetParent(transform);
			hexGo.isStatic = true;
		}
	}
		
}
