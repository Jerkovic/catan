using EventSystem;
using UnityEngine;

namespace ui
{
    public class HandleMoveRobber : MonoBehaviour
    {
        [SerializeField] private Transform _robber;
        
        private void OnEnable()
        {
            Events.OnClickHexagon.AddListener(MoveRobber);
        }
        
        private void OnDisable()
        {
            Events.OnClickHexagon.RemoveListener(MoveRobber);
        }
    
        private void MoveRobber(GameObject gameObject)
        {
            _robber.position = gameObject.transform.position;
            Debug.Log("Move robber to Tile " + gameObject.name);
        }
    }
}