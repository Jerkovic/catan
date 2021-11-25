using System.Collections;
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

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2.5f);
            Debug.Log("Start Game manager...");
            _game.Start();
            _game.GetBoard().SetRobberDesert();
        }

        public Catan.Game GetGame()
        {
            return _game;
        }
    }
}