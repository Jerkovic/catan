using System.Collections;
using EventSystem;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private Catan.Game _game;

        private void Awake()
        {
            Debug.Log("Awake Game manager...");
            _game = new Catan.Game();
        }

        private void OnEnable()
        {
            Events.OnClickHexagon.AddListener(RequestMoveRobber);
            Events.OnClickCorner.AddListener(RequestBuildSettlement);
            Events.OnClickEdge.AddListener(RequestBuildRoad);
            Events.OnClickSettlement.AddListener(RequestUpgradeSettlementToCityAtCorner);
        }

        private void OnDisable()
        {
            Events.OnClickHexagon.RemoveListener(RequestMoveRobber);
            Events.OnClickCorner.RemoveListener(RequestBuildSettlement);
            Events.OnClickSettlement.RemoveListener(RequestUpgradeSettlementToCityAtCorner);
            Events.OnClickEdge.RemoveListener(RequestBuildRoad);
        }
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Start Game manager...");
            _game.Start();
        }
    
        private void RequestBuildSettlement(GameObject go)
        {
            var hashCode = int.Parse(go.name);
            GetGame().BuildSettlementAtCorner(hashCode);
        }
        
        private void RequestUpgradeSettlementToCityAtCorner(GameObject go)
        {
            var hashCode = go.GetComponent<Settlement>().CornerHash;
            GetGame().UpgradeSettlementToCityAtCorner(hashCode);
        }
    
        private void RequestBuildRoad(Object go)
        {
            var hashCode = int.Parse(go.name);
            GetGame().BuildRoadAtEdge(hashCode);
        }

        public void NextTurn()
        {
            GetGame().NextTurn();
        }
    
        public void RollDices()
        {
            GetGame().RollDices();
        }
    
        private void RequestMoveRobber(GameObject go)
        {
            Debug.Log("Move robber to tile " + go.name);
            // Todo implement move robber
        }        
        
        public Catan.Game GetGame()
        {
            return _game;
        }
    }
}