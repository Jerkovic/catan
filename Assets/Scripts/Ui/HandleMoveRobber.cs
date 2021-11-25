using Catan;
using EventSystem;
using UnityEngine;

namespace Ui
{
    public class HandleMoveRobber : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnRobberMove.AddListener(MoveRobberPiece);
        }
        
        private void OnDisable()
        {
            Events.OnRobberMove.RemoveListener(MoveRobberPiece);
        }
    
        private void MoveRobberPiece(HexTile hexTile)
        {
            Debug.Log("move robber to world pos " + hexTile.ToWorldCoordinates());
            transform.position = hexTile.ToWorldCoordinates();
        }
    }
}