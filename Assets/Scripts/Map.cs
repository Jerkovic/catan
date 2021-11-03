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
		var radius = 3;
		CubicHexCoord[] board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), radius);
		foreach (CubicHexCoord coord in board)
		{
			float xPos = coord._x * xOffset;
			float zPos = coord._z * zOffset;
			
			if( coord._z > 0 && coord._z % 2 == 1)
			{
				xPos += xOffset / 2f + (xOffset * (coord._z / 2));
			}
			if( coord._z > 0 && coord._z % 2 == 0)
			{
				xPos += xOffset + (xOffset * (coord._z / 2));;
				xPos -= xOffset;
			}
			
			if( coord._z < 0 &&  (coord._z * -1) % 2 == 1)
			{
				xPos -= (xOffset / 2f) + (xOffset * (-coord._z / 2));
			}
			
			if( coord._z < 0 && (coord._z * -1) % 2 == 0) // even 
			{
				xPos -= (xOffset) + (xOffset * (-coord._z / 2));
				xPos += xOffset;
			}
			
			GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);				
			hex_go.name = "Hex__" + coord.ToString();
			hex_go.transform.SetParent(this.transform);				
			hex_go.isStatic = false;
		}
		Debug.Log(board.Length);
		
		/*
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

				float xPos = x * xOffset;

				// Are we on an odd row?
				if( y % 2 == 1 ) {
					xPos += xOffset/2f;
				}

				GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3( xPos,0, y * zOffset  ), Quaternion.identity  );				
				hex_go.name = "Hex_" + x + "_" + y;

				hex_go.transform.SetParent(this.transform);
				
				hex_go.isStatic = true;

			}
		} */

	}
		
}
