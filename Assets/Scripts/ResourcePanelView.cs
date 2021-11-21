using System;
using TMPro;
using UnityEngine;

public class ResourcePanelView : MonoBehaviour
{
    // should be listening on ResourceUpdateEvent
    private void Start()
    {
        // todo cache
        var wood = transform.GetChild(0);
        var text = wood.GetComponentsInChildren<TextMeshProUGUI>();
        text[0].SetText("12");
    }
}
