using System.Collections.Generic;
using Catan;
using DG.Tweening;
using EventSystem;
using UnityEngine;

namespace Ui
{
    public class HandleTileProducing : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnTilesProducing.AddListener(Highlighter);
        }

        private void OnDisable()
        {
            Events.OnTilesProducing.RemoveListener(Highlighter);
        }

        private void Highlighter(List<HexTile> tiles)
        {
            foreach (var tile in tiles)
            {
                var trans = GameObject.Find(tile.GetHashCode().ToString()).transform;
                var sequence = DOTween.Sequence()
                    .Join(trans.DOPunchScale(new Vector3(-0.5f, 0f, -0.5f), .5f, 3, 2.9f));
                sequence.SetLoops(2, LoopType.Yoyo);    
            }
        }
    }
}