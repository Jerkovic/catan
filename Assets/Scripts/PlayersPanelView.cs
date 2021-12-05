using System.Collections.Generic;
using Catan;
using EventSystem;
using UnityEngine;
using Managers;
using TMPro;
using UI;
using UnityEngine.UI;

public class PlayersPanelView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPanelPrefab;
    
    private void OnEnable()
    {
        Events.OnPlayerTurnChanged.AddListener(ActivatePlayerInPanel);
        Events.OnGameStarted.AddListener(CreatePlayerPanels);
    }
        
    private void OnDisable()
    {
        Events.OnPlayerTurnChanged.RemoveListener(ActivatePlayerInPanel);
    }

    private void Start()
    {

    }

    private void CreatePlayerPanels(List<Player> players)
    {
        foreach (var player in players)
        {
            CreatePlayerPanel(player);
        }
    }

    private void CreatePlayerPanel(Player player)
    {
        var go  = Instantiate(playerPanelPrefab, transform, true);
        go.name = player.Guid;
        var image = go.GetComponentsInChildren<Image>();
        image[1].color = player.Color;
        var text = go.GetComponentsInChildren<TMP_Text>();
        text[0].SetText(player.Name);
        text[1].SetText(player.Guid);
    }

    private void ActivatePlayerInPanel(Player player)
    {
        ResetIndicators();
        // remember to reset all first
        Debug.Log("It is " + player.Name + " turn!");
        var panel = transform.Find(player.Guid);
        var indicator = panel.Find("TurnIndicator");
        var image = indicator.GetComponent<Image>();
        image.color = Color.yellow;
    }

    private void ResetIndicators()
    {
        var indicators = transform.GetComponentsInChildren<TurnIndicator>();
        foreach (var indicator in indicators)
        {
            indicator.GetComponent<Image>().color = Color.white;
        }
        
    }
}
    
