using UnityEngine;
using logic;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;
	public GameObject cornerPlaceholderPrefab;
	private Board board;
		
	private void Start ()
	{
		board = new Board(3);
		
		foreach (var tile in board.tiles)
		{			
			var hexGo = Instantiate(hexPrefab, tile.ToWorldCoordinates(), Quaternion.identity);						 
			hexGo.name = tile.GetHashCode().ToString();
			var meshRenderer = hexGo.GetComponentInChildren<MeshRenderer>();
			meshRenderer.material.color = tile.color;
			hexGo.tag = "Hexagon";
			hexGo.transform.SetParent(transform);
			hexGo.isStatic = true;
		}		
		
		// define test corner placement 
		initCorner(206839, 207345, 206817);
		initCorner(206839, 206817, 206311);
		initCorner(206839, 206311, 206333);
		initCorner(206839, 206333, 206861);
		initCorner(206839, 206861, 207367);
		initCorner(206839, 207367, 207345);
		
		// next ring
		initCorner(206817, 207345, 207323);
		initCorner(206817, 207323, 206795);
		initCorner(206817, 206795, 206289);		
		initCorner(206817, 206289, 206311);
		
		initCorner(206311, 206289, 205783);
		initCorner(206311, 205783, 205805);
		initCorner(206311, 206333, 205805);
		initCorner(206333, 205805, 205827);
		initCorner(206333, 205827, 206355);
		initCorner(206333, 206355, 206861);
		
		initCorner(206861, 206355, 206883);
		initCorner(206861, 206883, 207389);		
		initCorner(206861, 207367, 207389);
		
		initCorner(207367, 207389, 207895);
		initCorner(207367, 207895, 207873);
		initCorner(207367, 207873, 207345);
		initCorner(207345, 207873, 207851);
		initCorner(207345, 207851, 207323);		
		// End 2:nd ring
		
		
		
	}

	private void initCorner(int hex1, int hex2, int hex3)
	{
		var tile1 = board.getTileByHashCode(hex1);
		var tile2 = board.getTileByHashCode(hex2);
		var tile3 = board.getTileByHashCode(hex3);
		var t1 = tile1.ToWorldCoordinates();
		var t2 = tile2.ToWorldCoordinates();
		var t3 = tile3.ToWorldCoordinates();
		var centerX = (t1.x + t2.x + t3.x) / 3;
		var centerZ = (t1.z + t2.z + t3.z) / 3;
		var corner = Instantiate(cornerPlaceholderPrefab, new Vector3(centerX, 0, centerZ), Quaternion.identity);
		corner.name = "Corner";
		corner.tag = "Corner";
		corner.transform.SetParent(transform);
		corner.isStatic = true;
	}
		 
}
