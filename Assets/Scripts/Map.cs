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
		
		CubicHexCoord[] water = CubicHexCoord.Ring(new CubicHexCoord(0, 0, 0), radius);
		
		// this is the render method 
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
			var name = "Hex__" + coord.ToString();
			if (Array.IndexOf(water, coord) > -1)
			{
				name = "Water";
				MeshRenderer mr = hex_go.GetComponentInChildren<MeshRenderer>();
				mr.material.color = Color.blue;
			}
			else {
				MeshRenderer mr = hex_go.GetComponentInChildren<MeshRenderer>();
				mr.material.color = Color.green;
			}
			hex_go.name = name;
			hex_go.transform.SetParent(this.transform);
			hex_go.isStatic = true;
		}
	}
		
}
