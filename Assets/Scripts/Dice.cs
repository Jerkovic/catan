using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public void Roll()
    {
        int dice1 = Random.Range(1, 6);
        int dice2 = Random.Range(1, 6);
        int sum = dice1 + dice2;        
        Events.onRollDice.Invoke(sum);
    }
}
