using EventSystem;
using Sound;
using UnityEngine;

namespace UI
{
    public class HandleSettlementBuilt : MonoBehaviour
    {
        public AudioEvent audioClip;
        public AudioEvent audioClipHarbor;
        public GameObject villagePrefab;

        private void OnEnable()
        {
            Events.OnSettlementBuilt.AddListener(PlaceSettlement);
        }

        private void OnDisable()
        {
            Events.OnSettlementBuilt.RemoveListener(PlaceSettlement);
        }

        private void PlaceSettlement(SettlementBuilt settlementBuild)
        {
            var corner = settlementBuild.Corner;
            var player = settlementBuild.Player;
            var go = GameObject.Find(corner.GetHashCode().ToString());
            var position = go.transform.position;
            var offset = new Vector3(position.x, 0.095f, position.z);
            var village = Instantiate(villagePrefab, offset, Quaternion.identity);
            village.GetComponent<Settlement>().CornerHash = corner.GetHashCode();
            var mr = village.GetComponentInChildren<MeshRenderer>();
            mr.enabled = true;
            mr.material.color = player.Color;
            
            var audioSource = go.AddComponent<AudioSource>();
            if (corner.IsPort())
            {
                audioClipHarbor.Play(audioSource);
            }
            else
            {
                audioClip.Play(audioSource);    
            }
        }
    }
}