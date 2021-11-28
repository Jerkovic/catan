using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
	[SerializeField] private new Camera camera;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			var ray = camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hitInfo))
			{
				var ourHitObject = hitInfo.collider.transform;

				if (ourHitObject != null && ourHitObject.CompareTag("HexagonMesh"))
				{
					Events.OnClickHexagon.Invoke(ourHitObject.parent.gameObject);
				}

				if (ourHitObject != null && ourHitObject.CompareTag("Edge"))
				{
					Events.OnClickEdge.Invoke(ourHitObject.gameObject);
				}

				if (ourHitObject != null && ourHitObject.CompareTag("Corner"))
				{
					Events.OnClickCorner.Invoke(ourHitObject.gameObject);
				}
			}
		}
	}
}
