using System;
using UnityEngine;
using System.Collections;
using logic;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;

	float xOffset = 0.882f;
	float zOffset = 0.764f;

	// Use this for initialization
	void Start ()
	{
		Board board = new Board(4);
		foreach (HexTile tile in board.tiles)
		{
			var coord = tile.coordinate;
			float xPos = coord._x * xOffset;
			float zPos = coord._z * zOffset;
			
			if( coord._z > 0 && coord._z % 2 == 1)
			{
				xPos += xOffset / 2f + (xOffset * (coord._z / 2));
			}
			if( coord._z > 0 && coord._z % 2 == 0)
			{
				xPos += (xOffset * (coord._z / 2));;
			}
			
			if( coord._z < 0 &&  (coord._z * -1) % 2 == 1)
			{
				xPos -= (xOffset / 2f) + (xOffset * (-coord._z / 2));
			}
			
			if( coord._z < 0 && (coord._z * -1) % 2 == 0) // even 
			{
				xPos -= (xOffset * (-coord._z / 2));
			}
			
			GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
			
			var name = "Hex__" + tile.getId();
			hex_go.name = name;
			MeshRenderer mr = hex_go.GetComponentInChildren<MeshRenderer>();
			mr.material.color = tile.color;
			hex_go.transform.SetParent(this.transform);
			hex_go.isStatic = true;
		}
	}
		
}
