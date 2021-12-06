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
            Events.OnRollDice.AddListener(DisplayRollDiceResult);
        }
        
        private void OnDisable()
        {
            Events.OnRollDice.RemoveListener(DisplayRollDiceResult);
        }
    
        private void DisplayRollDiceResult(int num)
        {
            // sound test
            audioClip.Play(audioSource);
            label.text = num.ToString();
        }
    
    }
}
