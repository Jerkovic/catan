using EventSystem;
using TMPro;
using UnityEngine;

public class HandleDiceResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    private void OnEnable()
    {
        Events.onRollDice.AddListener(DisplayRollDiceResult);
    }
        
    private void OnDisable()
    {
        Events.onRollDice.RemoveListener(DisplayRollDiceResult);
    }
    
    private void DisplayRollDiceResult(int num)
    {
        label.text = num.ToString();
        Debug.Log("sum " + num);
    }
    
}
