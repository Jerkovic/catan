using UnityEngine;
using logic;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;
	public GameObject cornerPlaceholderPrefab;
	private Board _board;
		
	private void Start ()
	{
		/*
		 * corners 54, edges 72, tiles 19, chits 18, ports 9
		 */
		_board = new Board(3);

		foreach (var tile in _board.tiles)
		{
			InitHexTile(tile); // 19
		}
		
		foreach (var corner in _board.corners)
		{			
			InitCorner(corner); // 54 
		}
		
		// Undirected graph with corner vertices and edges
		// Graph graph = new BoardGraph();
		// graph.addCornerEdge(0,1); // between two corners
	}

	private void InitHexTile(HexTile tile)
	{
		var hexGo = Instantiate(hexPrefab, tile.ToWorldCoordinates(), Quaternion.identity);						 
		hexGo.name = tile.GetHashCode().ToString();
		var meshRenderer = hexGo.GetComponentInChildren<MeshRenderer>();
		meshRenderer.material.color = tile.color;
		hexGo.tag = "Hexagon";
		hexGo.transform.SetParent(transform);
		hexGo.isStatic = true;
	}

	private void InitCorner(Corner corner)
	{
		var tile1 = _board.GetTileByHashCode(corner.hex1);
		var tile2 = _board.GetTileByHashCode(corner.hex2);
		var tile3 = _board.GetTileByHashCode(corner.hex3);
		var t1 = tile1.ToWorldCoordinates();
		var t2 = tile2.ToWorldCoordinates();
		var t3 = tile3.ToWorldCoordinates();
		var centerX = (t1.x + t2.x + t3.x) / 3;
		var centerZ = (t1.z + t2.z + t3.z) / 3;
		var cornerObj = Instantiate(cornerPlaceholderPrefab, new Vector3(centerX, 0, centerZ), Quaternion.identity);
		cornerObj.name = corner.GetHashCode().ToString();
		cornerObj.tag = "Corner";
		cornerObj.transform.SetParent(transform);
		cornerObj.isStatic = true;
	}
		 
}
