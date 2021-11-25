using EventSystem;
using Managers;
using UnityEngine;

namespace Ui
{
    public class HandleTileClick : MonoBehaviour
    {
        private void OnEnable()
        {
            Events.OnClickHexagon.AddListener(Test);
        }

        private void OnDisable()
        {
            Events.OnClickHexagon.RemoveListener(Test);
        }

        private void Test(GameObject go)
        {
            Debug.Log("Clicked tile " + go.name);
            var hexCode = int.Parse(go.name);
            var corners = GameManager.Instance.GetGame().GetBoard().GetCornersByTile(hexCode);
            Debug.Log(corners.Count);
        }
    }
}