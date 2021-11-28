using EventSystem;
using Managers;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void OnEnable()
    {
        Events.OnClickHexagon.AddListener(RequestMoveRobber);
        Events.OnClickCorner.AddListener(RequestBuildSettlement);
        Events.OnClickEdge.AddListener(RequestBuildRoad);
    }

    private void OnDisable()
    {
        Events.OnClickHexagon.RemoveListener(RequestMoveRobber);
        Events.OnClickCorner.RemoveListener(RequestBuildSettlement);
        Events.OnClickEdge.RemoveListener(RequestBuildRoad);
    }
    
    private void RequestBuildSettlement(GameObject go)
    {
        var hashCode = int.Parse(go.name);
        GameManager.Instance.GetGame().BuildSettlementAtCorner(hashCode);
    }
    
    private void RequestBuildRoad(Object go)
    {
        var hashCode = int.Parse(go.name);
        GameManager.Instance.GetGame().BuildRoadAtEdge(hashCode);
    }

    public void NextTurn()
    {
        GameManager.Instance.GetGame().NextTurn();
    }
    
    public void RollDices()
    {
        GameManager.Instance.GetGame().RollDices();
    }
    
    private void RequestMoveRobber(GameObject go)
    {
        Debug.Log("Move robber to tile " + go.name);
        // Todo implement move robber
    }
}

