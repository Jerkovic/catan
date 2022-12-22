using System.Collections.Generic;
using Catan;
using DG.Tweening;
using EventSystem;
using UnityEngine;

namespace UI
{
    public class HandleLongestRoad : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnLongestRoad.AddListener(AnimateEdges);
        }

        private void OnDisable()
        {
            Events.OnLongestRoad.RemoveListener(AnimateEdges);
        }

        private void AnimateEdges(List<Edge> edges)
        {
            // Debug.Log("Animate Edges: " + edges.Count);
            foreach (var edge in edges)
            {
                var trans = GameObject.Find(edge.GetHashCode().ToString()).transform;
                var sequence = DOTween.Sequence()
                    .Join(trans.DOPunchScale(new Vector3(.05f, .05f, .05f), .5f, 1, 2.9f));
                sequence.SetLoops(1, LoopType.Yoyo);    
            }
            
        }
    }
}