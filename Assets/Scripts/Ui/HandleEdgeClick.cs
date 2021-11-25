using EventSystem;
using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleEdgeClick : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnClickEdge.AddListener(ChangeColor);
        }
        
        private void OnDisable()
        {
            Events.OnClickEdge.RemoveListener(ChangeColor);
        }
    
        private void ChangeColor(GameObject gameObject)
        {
            Debug.Log("Clicked edge to place road " + gameObject.name);
            var mr = gameObject.GetComponentInChildren<MeshRenderer>();
            mr.enabled = true;
            mr.material.color = Color.blue;
            // GameManager.Instance.PlaceRoad()
        }
    
    }
}