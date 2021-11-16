using UnityEngine;
using logic;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;
	public GameObject cornerPlaceholderPrefab;
	private Board board;
		
	private void Start ()
	{
		board = new Board(3);
		/*
		 * corners 54 edges 72 tiles 19 chits 18 ports 9
		 */
		
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
		
		// corner placement center tile 
		initCorner(206839, 207345, 206817);
		initCorner(206839, 206817, 206311);
		initCorner(206839, 206311, 206333);
		initCorner(206839, 206333, 206861);
		initCorner(206839, 206861, 207367);
		initCorner(206839, 207367, 207345);
		
		// next ring total #18
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
		
		// Start 3 ring # of corners 30
		initCorner(206795, 207323, 207301); 
		initCorner(206795, 207301, 206773); 
		initCorner(206267, 206795, 206773);
		initCorner(206289, 206795, 206267);
		initCorner(205761, 206289, 206267);
		initCorner(205783, 205761, 206289);
		initCorner(205255, 205783, 205761);
		initCorner(205277, 205783, 205255);
		initCorner(205805, 205277, 205783); 
		initCorner(205299, 205805, 205277);
		initCorner(205299, 205805, 205827); 
		initCorner(205321, 205827, 205299); 
		initCorner(205321, 205827, 205849); 		
		initCorner(206355, 205827, 205849); 
		initCorner(206355, 205849, 206377); 
		initCorner(206355, 206377, 206883);
		initCorner(206883, 206377, 206905);
		initCorner(206883, 207411, 206905);			
		initCorner(206883, 207411, 207389); 
		initCorner(207389, 207917, 207411);
		initCorner(207389, 207917, 207895);
		initCorner(207895, 208423, 207917);		
		initCorner(207895, 208423, 208401);
		initCorner(207895, 207873, 208401);
		initCorner(207873, 208401, 208379);
		initCorner(207873, 207851, 208379);
		initCorner(207851, 208379, 208357); 
		initCorner(207851, 208357, 207829);
		initCorner(207323, 207851, 207829);
		initCorner(207323, 207829, 207301);		
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
