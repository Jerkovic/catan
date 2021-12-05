using EventSystem;
using Sound;
using UnityEngine;

namespace UI
{
    public class HandleRoadBuilt : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioEvent audioClip;
        
        private void OnEnable()
        {
            Events.OnRoadBuilt.AddListener(PlaceRoad);
        }

        private void OnDisable()
        {
            Events.OnRoadBuilt.RemoveListener(PlaceRoad);
        }

        private void PlaceRoad(RoadBuilt roadBuilt)
        {
            // sound test
            audioClip.Play(audioSource);
            
            var edge = roadBuilt.Edge;
            var player = roadBuilt.Player;
            var go = GameObject.Find(edge.GetHashCode().ToString());
            var mr = go.GetComponentInChildren<MeshRenderer>();
            mr.enabled = true;
            mr.material.color = player.Color;
        }
    }
}