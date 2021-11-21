using UnityEngine;
using Managers;
using TMPro;
using UnityEngine.UI;

public class PlayersPanelView : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPanelPrefab;

    private void Start()
    {
        var game = GameManager.Instance.GetGame();
        foreach (var player in game.GetPlayers())
        {
            var go  = Instantiate(playerPanelPrefab, transform, true);
            go.name = player.Name;
            var image = go.GetComponentsInChildren<Image>();
            image[1].color = player.Color;
            var text = go.GetComponentsInChildren<TextMeshProUGUI>();
            text[0].SetText(player.Name);
        }
    }
}
    
