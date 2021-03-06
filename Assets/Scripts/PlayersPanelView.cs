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
        Events.OnPlayerDataChanged.AddListener(UpdatePlayerData);
    }
        
    private void OnDisable()
    {
        Events.OnPlayerTurnChanged.RemoveListener(ActivatePlayerInPanel);
        Events.OnGameStarted.RemoveListener(CreatePlayerPanels);
        Events.OnResourcesUpdate.RemoveListener(UpdateResourceCards);
        Events.OnPlayerDataChanged.RemoveListener(UpdatePlayerData);
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
        text[1].SetText("Points: 0");
        text[2].SetText("Resources: " + player.GetResourcesCount().ToString());
        text[3].SetText("Cards: " + player.GetDevelopmentCardsCount().ToString());
    }

    private void ActivatePlayerInPanel(Player player)
    {
        PanelManager.Instance.ShowMessage($"It's {player.Name}'s turn!");
        
        ResetIndicators();
        var panel = transform.Find(player.Guid);
        var indicator = panel.Find("TurnIndicator");
        indicator.gameObject.SetActive(true);
        var image = indicator.GetComponent<Image>();
        image.color = Color.yellow;
    }

    private void ResetIndicators()
    {
        var indicators = transform.GetComponentsInChildren<TurnIndicator>();
        foreach (var indicator in indicators)
        {
            indicator.gameObject.SetActive(false);
            indicator.GetComponent<Image>().color = Color.black;
        }
    }
    
    private void UpdateResourceCards(ResourcesGained resourcesGained)
    {
        var player = resourcesGained.Player;
        UpdatePlayerData(player);
    }
    
    private void UpdatePlayerData(Player player)
    {
        var panel = transform.Find(player.Guid);
        var text = panel.GetComponentsInChildren<TMP_Text>();
        text[1].SetText("Points: " + player.GetPoints().ToString());
        text[2].SetText("Resources: " + player.GetResourcesCount().ToString());
        text[3].SetText("Cards: " + player.GetDevelopmentCardsCount().ToString());
    }
}
    
