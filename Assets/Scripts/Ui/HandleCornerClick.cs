using EventSystem;
using UnityEngine;

namespace Ui
{
    public class HandleCornerClick : MonoBehaviour
    {
        public GameObject villagePrefab;
        public GameObject cityPrefab;
        private void OnEnable()
        {
            Events.OnClickCorner.AddListener(ChangeColor);
        }
        
        private void OnDisable()
        {
            Events.OnClickCorner.RemoveListener(ChangeColor);
        }
    
        private void ChangeColor(GameObject gameObject)
        {
            Debug.Log("clicked to place test settlement on corner " + gameObject.name);
            var position = gameObject.transform.position;
            var offset = new Vector3(position.x, 0.095f, position.z);
            Instantiate(cityPrefab, offset, Quaternion.identity);
        }
    
    }
}