using System.Collections.Generic;
using Catan;
using Catan.Resources;
using DG.Tweening;
using EventSystem;
using Managers;
using TMPro;
using UI.Resource;
using UnityEngine;

public class ResourcePanelView : MonoBehaviour
{
    private TMP_Text _woodText;
    private TMP_Text _brickText;
    private TMP_Text _sheepText;
    private TMP_Text _wheatText;
    private TMP_Text _oreText;
    
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject resourceUiPrefab;
    [SerializeField] private Transform uiCanvas;
    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        _woodText = transform.GetChild(0).GetComponentsInChildren<TMP_Text>()[0];
        _brickText = transform.GetChild(1).GetComponentsInChildren<TMP_Text>()[0];
        _sheepText = transform.GetChild(2).GetComponentsInChildren<TMP_Text>()[0];
        _wheatText = transform.GetChild(3).GetComponentsInChildren<TMP_Text>()[0];
        _oreText = transform.GetChild(4).GetComponentsInChildren<TMP_Text>()[0];
    }

    private void OnEnable()
    {
        Events.OnResourcesUpdate.AddListener(AnimateResources);
        Events.OnPlayerTurnChanged.AddListener(UpdateUI);
    }

    private void OnDisable()
    {
        Events.OnResourcesUpdate.RemoveListener(AnimateResources);
        Events.OnPlayerTurnChanged.AddListener(UpdateUI);
    }
    
    private void AnimateResources(ResourcesGained resourcesGained)
    {
        var player = resourcesGained.Player;
        var resources = resourcesGained.Resources;
        
        if (GameManager.Instance.GetGame().GetTurnPlayerGuid() == player.Guid)
        {
            foreach (var item in resources)
            {
                var tile = item.Key;
                var amount = item.Value;
                SpawnResource(tile, amount, player);
            }
        }
    }

    private void SpawnResource(HexTile tile, int amount, Player player)
    {
        var screenPos = camera.WorldToScreenPoint(tile.ToWorldCoordinates());
        var res = Instantiate(resourceUiPrefab, screenPos, Quaternion.identity);
        res.GetComponent<ResourceCard>().resource = tile.GetResourceType();
        res.transform.SetParent(uiCanvas);
        res.transform.localScale = Vector3.zero;
        var sequence = DOTween.Sequence();
        sequence.Join(res.transform.DOScale(new Vector3(1f, 1f, 1f), 1.5f));
        sequence.Join(res.transform.DOMove(targetTransform.position, 2f).SetEase(Ease.InOutExpo));
        sequence.OnComplete(() =>
        {
            Destroy(res);
            UpdateUI(player);
        });
    }

    private void UpdateUI(Player player)
    {
        // temporary code until we find a better way to GetTurnPlayerGuid
        if (GameManager.Instance.GetGame().GetTurnPlayerGuid() == player.Guid)
        {
            UpdateResources(player);
        }
    } 

    private void UpdateResources(Player player)
    {
        var resources = player.GetResources();
        _woodText.SetText(resources[ResourceEnum.WOOD].ToString());
        _brickText.SetText(resources[ResourceEnum.BRICK].ToString());
        _sheepText.SetText(resources[ResourceEnum.SHEEP].ToString());
        _wheatText.SetText(resources[ResourceEnum.WHEAT].ToString());
        _oreText.SetText(resources[ResourceEnum.ORE].ToString());
    }
}