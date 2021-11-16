using System;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
	[SerializeField]
	private new Camera camera;
	
	private void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
				return;
			}
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out var hitInfo)) {
				var ourHitObject = hitInfo.collider.transform.parent.gameObject;
				if (ourHitObject != null && ourHitObject.CompareTag("Hexagon"))
				{
					// Events.OnClickHexagon.Invoke(int.Parse(ourHitObject.name));										
					MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
					mr.material.color = Color.red;
				}				
			}
		}		
	}	
}
