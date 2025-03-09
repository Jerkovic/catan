using System.Linq;
using Catan;
using EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace UI.DevCards
{
    public class HandleDevCardsUpdate : MonoBehaviour
    {
        public GameObject textPrefab;
        public Transform contentParent; // Panel's Content object
        private Button _btn; 

        private void OnEnable()
        {
            Events.OnPlayerDataChanged.AddListener(OnDevCardsUpdated);
            Events.OnPlayerTurnChanged.AddListener(OnDevCardsUpdated);
        }

        private void OnDisable()
        {
            Events.OnPlayerDataChanged.RemoveListener(OnDevCardsUpdated);
            Events.OnPlayerTurnChanged.RemoveListener(OnDevCardsUpdated);
        }

        private void OnDevCardsUpdated(Player player)
        {
            var cards = player.GetDevelopmentCards().ToList();
            
            var message = string.Join("\n", cards.Select(card => card.GetCardType()));
            Debug.Log(message);
            
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var card in cards)
            {
                var textObj = Instantiate(textPrefab, contentParent);
                _btn = textObj.transform.GetChild(0).GetComponentInChildren<Button>();
                if (!card.CanBePlayed())  
                {
                    _btn.interactable = false;
                } 

                var textComponent = textObj.GetComponent<TMP_Text>();
                textComponent.text = card.GetCardType() + '[' + card.GetCardStatus() + ']';
            }
            
        }
    }
}