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
		CubicHexCoord[] board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), 1);
		foreach (CubicHexCoord coord in board)
		{			
			float xPos = coord._x * xOffset;
			if( Math.Abs(coord._z) % 2 == 1 ) {
				xPos += xOffset/2f;
			}

			GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3( xPos,0, coord._z *  zOffset ), Quaternion.identity  );				
			hex_go.name = "Hex_" + coord.ToString();
			hex_go.transform.SetParent(this.transform);				
			hex_go.isStatic = true;
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
