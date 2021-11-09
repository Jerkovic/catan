using UnityEngine;

public class MouseManager : MonoBehaviour
{
	public Camera camera;
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
				return;
			}
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if( Physics.Raycast(ray, out hitInfo) ) {
				GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;
				Debug.Log("Clicked On: " + ourHitObject.name);
				MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
				mr.material.color = Color.red; 	
			}
		}
	}
}
