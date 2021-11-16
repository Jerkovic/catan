using EventSystem;
using TMPro;
using UnityEngine;

public class HandleDiceResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
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
        Debug.Log("sum " + num);
    }
    
}
