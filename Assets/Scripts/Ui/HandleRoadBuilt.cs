using Catan;
using EventSystem;
using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleRoadBuilt : MonoBehaviour
    {
        public GameObject roadPrefab;

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
            var edge = roadBuilt.Edge;
            var player = roadBuilt.Player;
            var go = GameObject.Find(edge.GetHashCode().ToString());
            var mr = go.GetComponentInChildren<MeshRenderer>();
            mr.enabled = true;
            mr.material.color = player.Color;
        }
    }
}