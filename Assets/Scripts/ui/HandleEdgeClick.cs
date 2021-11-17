using EventSystem;
using UnityEngine;

namespace ui
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
            var mr = gameObject.GetComponentInChildren<MeshRenderer>();
            mr.enabled = true;
            mr.material.color = Color.black;
        }
    
    }
}