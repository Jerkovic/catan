using System;
using EventSystem;
using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleCornerClick : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnClickCorner.AddListener(RequestBuildSettlement);
        }
        
        private void OnDisable()
        {
            Events.OnClickCorner.RemoveListener(RequestBuildSettlement);
        }
    
        private void RequestBuildSettlement(GameObject go)
        {
            Debug.Log("Clicked to place test settlement on corner " + go.name);
            var hashCode = int.Parse(go.name);
            GameManager.Instance.GetGame().BuildSettlementAtCorner(hashCode);
        }
    
    }
}