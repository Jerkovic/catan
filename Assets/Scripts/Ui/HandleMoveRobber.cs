using System;
using Catan;
using DG.Tweening;
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
            var newPos = hexTile.ToWorldCoordinates();
            transform.DOMove(newPos, 1.5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    Debug.Log("Robber is home");
                });
        }
    }
}