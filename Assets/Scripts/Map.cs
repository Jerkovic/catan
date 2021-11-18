using System.Collections.Generic;
using UnityEngine;
using logic;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;
	public GameObject cornerPlaceholderPrefab;
	public GameObject roadPlaceholderPrefab;

	[Header("HexTile Materials")]
	[SerializeField] private Material mountainMaterial;
	[SerializeField] private Material pastureMaterial;
	[SerializeField] private Material hillMaterial;
	[SerializeField] private Material fieldMaterial;
	[SerializeField] private Material forestMaterial;
	[SerializeField] private Material desertMaterial;
	[SerializeField] private Material seaMaterial;
	
	private Catan _catan;
	
	
		
	private void Start ()
	{
		/*
		 * Todo chits 18, ports 9
		 */
		_catan = new Catan();

		foreach (var tile in _catan.GetBoard().GetTiles())
		{
			InitHexTile(tile); // 19
		}
		
		foreach (var corner in _catan.GetBoard().GetCorners())
		{			
			InitCorner(corner); // 54 
		}
		
		foreach (var edge in _catan.GetBoard().GetEdges())
		{
			InitEdge(edge); // 72
		}
	}

	private void InitHexTile(HexTile tile)
	{
		var hexGo = Instantiate(hexPrefab, tile.ToWorldCoordinates(), Quaternion.identity);						 
		hexGo.name = tile.GetHashCode().ToString();
		var meshRenderer = hexGo.GetComponentInChildren<MeshRenderer>();

		if (tile.GetTileType() == TileTypeEnum.MOUNTAIN)
		{
			meshRenderer.material = mountainMaterial;
		}
		if (tile.GetTileType() == TileTypeEnum.PASTURE)
		{
			meshRenderer.material = pastureMaterial;
		}
		if (tile.GetTileType() == TileTypeEnum.HILL)
		{
			meshRenderer.material = hillMaterial;
		}
		if (tile.GetTileType() == TileTypeEnum.FIELD)
		{
			meshRenderer.material = fieldMaterial;
		}
		if (tile.GetTileType() == TileTypeEnum.FOREST)
		{
			meshRenderer.material = forestMaterial;
		}
		if (tile.GetTileType() == TileTypeEnum.DESERT)
		{
			meshRenderer.material = desertMaterial;
		}
		if (tile.GetTileType() == TileTypeEnum.SEA)
		{
			meshRenderer.material = seaMaterial;
		}
		
		hexGo.tag = "Hexagon";
		hexGo.transform.SetParent(transform);
		hexGo.isStatic = true;
	}
	
	private void InitEdge(Edge edge)
	{
		var corner1 = edge.GetLeftCorner();
		var corner2 = edge.GetRightCorner();
		var pos1 = GetCornerWorldCoordinates(corner1);
		var pos2 = GetCornerWorldCoordinates(corner2);
		var rotation = Quaternion.LookRotation(pos1 - pos2, Vector3.forward);
		var centerX = (pos1.x + pos2.x) / 2;
		var centerZ = (pos1.z + pos2.z) / 2;
		var pos = new Vector3(centerX, .07f, centerZ);
		var edgeObj = Instantiate(roadPlaceholderPrefab, pos, rotation);
		edgeObj.name = edge.GetHashCode().ToString(); 
		edgeObj.tag = "Edge";
		edgeObj.transform.SetParent(transform);
		edgeObj.isStatic = true;
	}

	private Vector3 GetHexTileWordCoordinates(int hashCode)
	{
		var tile = _catan.GetBoard().GetTileByHashCode(hashCode);
		return tile.ToWorldCoordinates();
	}

	private Vector3 GetCornerWorldCoordinates(Corner corner)
	{
		var t1 = GetHexTileWordCoordinates(corner.hex1);
		var t2 = GetHexTileWordCoordinates(corner.hex2);
		var t3 = GetHexTileWordCoordinates(corner.hex3);
		var centerX = (t1.x + t2.x + t3.x) / 3;
		var centerZ = (t1.z + t2.z + t3.z) / 3;
		return new Vector3(centerX, 0, centerZ);
	}
	
	private void InitCorner(Corner corner)
	{
		var cornerObj = Instantiate(cornerPlaceholderPrefab, GetCornerWorldCoordinates(corner), Quaternion.identity);
		cornerObj.name = corner.GetHashCode().ToString();
		cornerObj.tag = "Corner";
		cornerObj.transform.SetParent(transform);
		cornerObj.isStatic = true;
	}
		 
}
