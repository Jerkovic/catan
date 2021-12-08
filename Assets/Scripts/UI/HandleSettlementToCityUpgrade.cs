using System.Linq;
using EventSystem;
using Sound;
using UnityEngine;

namespace UI
{
    public class HandleSettlementToCityUpgrade : MonoBehaviour
    {
        public AudioEvent audioClip;
        public GameObject cityPrefab;

        private void OnEnable()
        {
            Events.OnSettlementUpgradeToCity.AddListener(UpgradeVillageToCity);
        }

        private void OnDisable()
        {
            Events.OnSettlementBuilt.RemoveListener(UpgradeVillageToCity);
        }

        private void UpgradeVillageToCity(SettlementBuilt settlementBuild)
        {
            var corner = settlementBuild.Corner;
            var player = settlementBuild.Player;
            var settlements = FindObjectsOfType<Settlement>();
            var oldSettlement = settlements.FirstOrDefault((s) => s.CornerHash == corner.GetHashCode());

            if (oldSettlement != null)
            {
                Destroy(oldSettlement.gameObject);    
            }
            
            var go = GameObject.Find(corner.GetHashCode().ToString());
            var position = go.transform.position;
            var offset = new Vector3(position.x, 0.095f, position.z);
            var city = Instantiate(cityPrefab, offset, Quaternion.identity);
            var mr = city.GetComponentInChildren<MeshRenderer>();
            mr.enabled = true;
            mr.material.color = player.Color;
            
            var audioSource = go.AddComponent<AudioSource>();
            audioClip.Play(audioSource);
        }
    }
}