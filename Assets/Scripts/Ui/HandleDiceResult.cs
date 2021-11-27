using EventSystem;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class HandleDiceResult : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
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
            label.text = num.ToString();
        }
    
    }
}
