﻿using System;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
	[SerializeField]
	private new Camera camera;

	private List<int> outer;

	private void Start()
	{
		outer = new List<int>();		
		Debug.Log(outer.Count);
	}

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
					// move to controller 
					// Debug.Log("Clicked On: " + ourHitObject.name);
					outer.Add(int.Parse(ourHitObject.name));
					MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();
					mr.material.color = Color.red;
				}
				 	
			}
		}
	
		if (Input.GetMouseButtonDown(1))
		{
			var result = "";
			Debug.Log(outer.Count);
			var i = 0;
			foreach (var item in outer)
			{
				result += item + ", ";
				i++;
				if (i % 3 == 0) result += "\n";
			}
			Debug.Log(result);
		}
	}
	
}
