using System.Linq;
using Catan;
using EventSystem;
using UnityEngine;

namespace UI.DevCards
{
    public class HandleDevCardsUpdate : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnPlayerDataChanged.AddListener(OnDevCardsUpdated);
        }

        private void OnDisable()
        {
            Events.OnPlayerDataChanged.RemoveListener(OnDevCardsUpdated);
        }

        private static void OnDevCardsUpdated(Player player)
        {
            var cards = player.GetDevelopmentCards();
            var message = string.Join("\n", cards.Select(card => card.GetCardType()));
            Debug.Log(message);
        }
    }
}