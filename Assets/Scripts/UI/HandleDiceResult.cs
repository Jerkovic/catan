using EventSystem;
using Sound;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HandleDiceResult : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        public AudioSource audioSource;
        public AudioEvent audioClip;
        
        private void OnEnable()
        {
            Events.OnRolledDices.AddListener(DisplayRollDiceResult);
        }
        
        private void OnDisable()
        {
            Events.OnRolledDices.RemoveListener(DisplayRollDiceResult);
        }
    
        private void DisplayRollDiceResult(DicesRolled dicesRolled)
        {
            audioClip.Play(audioSource);
            var text = $"{dicesRolled.Total.ToString()} [{dicesRolled.Dice1}][{dicesRolled.Dice2}]";
            label.text = text;
        }
    }
}
