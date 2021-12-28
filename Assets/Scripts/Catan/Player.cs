using System.Collections.Generic;
using System.Linq;
using Catan.Resources;
using UnityEngine;

namespace Catan
{
    public class Player
    {
        public string Name { get; set; }
        public string Guid { get; set; }
        public Color Color { get; set; }
        public int knights; // knight cards
        public int victoryPoints; // cards

        private readonly Dictionary<ResourceEnum, int> _resources;

        public Player(Color color, string name)
        {
            Color = color;
            Name = name;
            Guid = Game.GenerateGuid();
            _resources = new Dictionary<ResourceEnum, int>()
            {
                {ResourceEnum.WOOD, 0},
                {ResourceEnum.BRICK, 0},
                {ResourceEnum.SHEEP, 0},
                {ResourceEnum.WHEAT, 0},
                {ResourceEnum.ORE, 0}
            };
        }

        public Dictionary<ResourceEnum, int> GetResources()
        {
            return _resources;
        }

        public int GetResourcesCount()
        {
            return _resources.Values.Sum();
        }

        public void AddResource(ResourceEnum resource, int amount)
        {
            if (resource == ResourceEnum.NONE) return;
            _resources[resource] += amount;
        }
    }
}