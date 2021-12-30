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

        private void DevCard(string cardName)
        {
            PanelManager.Instance.ShowMessage(cardName.Replace("_", " "));
            audioClip.Play(audioSource);
        }

    }
}