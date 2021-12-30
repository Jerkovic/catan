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
        Events.OnResourcesUpdate.AddListener(UpdateResourceCards);
        Events.OnPlayerDataChanged.AddListener(UpdateDevCards);
    }
        
    private void OnDisable()
    {
        Events.OnPlayerTurnChanged.RemoveListener(ActivatePlayerInPanel);
        Events.OnGameStarted.RemoveListener(CreatePlayerPanels);
        Events.OnResourcesUpdate.RemoveListener(UpdateResourceCards);
        Events.OnPlayerDataChanged.RemoveListener(UpdateDevCards);
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
        text[2].SetText("Resources: " + player.GetResourcesCount().ToString());
        text[3].SetText("Cards: " + player.GetDevelopmentCardsCount().ToString());
    }

    private void ActivatePlayerInPanel(Player player)
    {
        PanelManager.Instance.ShowMessage($"It's {player.Name}'s turn!");
        
        ResetIndicators();
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
    
    private void UpdateResourceCards(ResourcesGained resourcesGained)
    {
        var player = resourcesGained.Player;
        var panel = transform.Find(player.Guid);
        var text = panel.GetComponentsInChildren<TMP_Text>();
        text[2].SetText("Resources: " + player.GetResourcesCount().ToString());
    }
    
    private void UpdateDevCards(Player player)
    {
        var panel = transform.Find(player.Guid);
        var text = panel.GetComponentsInChildren<TMP_Text>();
        text[3].SetText("Cards: " + player.GetDevelopmentCardsCount().ToString());
    }
}
    
