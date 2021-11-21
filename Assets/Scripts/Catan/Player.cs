using System.Collections.Generic;
using Catan.Resources;
using UnityEngine;

namespace Catan
{
    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int knights; 
        public int victoryPoints;
        
        private Dictionary<ResourceEnum, int> _resources;

        public Player(Color color, string name)
        {
            Color = color;
            Name = name;
            _resources = new Dictionary<ResourceEnum, int>(){
                {ResourceEnum.WOOD, 0},
                {ResourceEnum.BRICK, 0},
                {ResourceEnum.SHEEP, 0},
                {ResourceEnum.WHEAT, 0},
                {ResourceEnum.ORE, 0}
            };
        }
        
    }
}