using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private Catan.Game _game;
        
        private void Awake()
        {
            Debug.Log("Starting Game manager...");
            _game = new Catan.Game();
        }

        public Catan.Game GetGame()
        {
            return _game;
        }
    }
}