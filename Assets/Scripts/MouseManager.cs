﻿using EventSystem;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    private UnityEngine.EventSystems.EventSystem _cachedEventSystem;

    private void Awake()
    {
        _cachedEventSystem = UnityEngine.EventSystems.EventSystem.current;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_cachedEventSystem.IsPointerOverGameObject())
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