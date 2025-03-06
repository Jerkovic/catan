using Catan.DevelopmentCards;
using EventSystem;
using Sound;
using UnityEngine;

namespace UI.DevCards
{
    public class HandleDevCardBought : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioEvent audioClip;
        
        private void OnEnable()
        {
            Events.OnDevCardBought.AddListener(DevCard);
        }

        private void OnDisable()
        {
            Events.OnDevCardBought.RemoveListener(DevCard);
        }

        private void DevCard(DevCard card)
        {
            PanelManager.Instance.ShowMessage(card.GetCardType());
            audioClip.Play(audioSource);
        }

    }
}