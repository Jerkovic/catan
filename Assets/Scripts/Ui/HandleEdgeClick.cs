using EventSystem;
using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleEdgeClick : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnClickEdge.AddListener(RequestBuildRoad);
        }
        
        private void OnDisable()
        {
            Events.OnClickEdge.RemoveListener(RequestBuildRoad);
        }
    
        private void RequestBuildRoad(Object go)
        {
            Debug.Log("Clicked edge to place road at " + go.name);
            var hashCode = int.Parse(go.name);
            GameManager.Instance.GetGame().BuildRoadAtEdge(hashCode);
        }
    
    }
}