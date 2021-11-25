using System;
using EventSystem;
using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleCornerClick : MonoBehaviour
    {
        public GameObject villagePrefab;
        public GameObject cityPrefab;
        private void OnEnable()
        {
            Events.OnClickCorner.AddListener(BuildCity);
        }
        
        private void OnDisable()
        {
            Events.OnClickCorner.RemoveListener(BuildCity);
        }
    
        private void BuildCity(GameObject go)
        {
            Debug.Log("clicked to place test settlement on corner " + go.name);
            var hexCode = int.Parse(go.name);
            var corner = GameManager.Instance.GetGame().GetBoard().GetCornerByHashCode(hexCode);
            Debug.Log(corner.IsPort().ToString());
            var position = go.transform.position;
            var offset = new Vector3(position.x, 0.095f, position.z);
            Instantiate(cityPrefab, offset, Quaternion.identity);
        }
    
    }
}