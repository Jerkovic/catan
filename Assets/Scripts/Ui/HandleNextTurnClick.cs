using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleNextTurnClick : MonoBehaviour
    {
        public void Click()
        {
            GameManager.Instance.GetGame().NextTurn();
        }
    }
}