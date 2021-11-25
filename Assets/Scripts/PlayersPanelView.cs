using Catan;
using EventSystem;
using UnityEngine;
using Managers;
using TMPro;
using Ui;
using UnityEngine.UI;

public class PlayersPanelView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPanelPrefab;
    
    private void OnEnable()
    {
        Events.OnPlayerTurnChanged.AddListener(ActivatePlayerInPanel);
    }
        
    private void OnDisable()
    {
        Events.OnPlayerTurnChanged.RemoveListener(ActivatePlayerInPanel);
    }

    private void Start()
    {
        var game = GameManager.Instance.GetGame();
        foreach (var player in game.GetPlayers())
        {
            var go  = Instantiate(playerPanelPrefab, transform, true);
            go.name = player.Guid;
            var image = go.GetComponentsInChildren<Image>();
            image[1].color = player.Color;
            var text = go.GetComponentsInChildren<TMP_Text>();
            text[0].SetText(player.Name);
        }
        Debug.Log("Players Panel ready!");
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
    
