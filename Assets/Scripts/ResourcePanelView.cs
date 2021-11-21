using System;
using TMPro;
using UnityEngine;

public class ResourcePanelView : MonoBehaviour
{
    // should be listening on ResourceUpdateEvent
    private TextMeshProUGUI _woodText;
    private TextMeshProUGUI _brickText;
    private TextMeshProUGUI _sheepText;
    private TextMeshProUGUI _wheatText;
    private TextMeshProUGUI _oreText;
    
    private void Start()
    {
        _woodText = transform.GetChild(0).GetComponentsInChildren<TextMeshProUGUI>()[0];
        _brickText = transform.GetChild(1).GetComponentsInChildren<TextMeshProUGUI>()[0];
        _sheepText = transform.GetChild(2).GetComponentsInChildren<TextMeshProUGUI>()[0];
        _wheatText = transform.GetChild(3).GetComponentsInChildren<TextMeshProUGUI>()[0];
        _oreText = transform.GetChild(4).GetComponentsInChildren<TextMeshProUGUI>()[0];

        UpdateUI();
    }

    private void UpdateUI()
    {
        _woodText.SetText("1");
        _brickText.SetText("2");
        _sheepText.SetText("4");
        _wheatText.SetText("5");
        _oreText.SetText("8");
    }
}
