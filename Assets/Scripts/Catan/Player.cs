using System.Collections.Generic;
using System.Linq;
using Catan.DevelopmentCards;
using Catan.Resources;
using UnityEngine;

namespace Catan
{
    public class Player
    {
        public string Name { get; set; }
        public string Guid { get; set; }
        public Color Color { get; set; }
        private int _points = 0;
        public int knights; // knight cards
        public int victoryPoints; // hidden cards of type victory_Point

        private readonly List<CardTypeEnum> _developmentCards;
        private readonly Dictionary<ResourceEnum, int> _resources;

        public Player(Color color, string name)
        {
            Color = color;
            Name = name;
            Guid = Game.GenerateGuid();
            _developmentCards = new List<CardTypeEnum>();
            
            _resources = new Dictionary<ResourceEnum, int>()
            {
                {ResourceEnum.WOOD, 12},
                {ResourceEnum.BRICK, 12},
                {ResourceEnum.SHEEP, 1},
                {ResourceEnum.WHEAT, 1},
                {ResourceEnum.ORE, 1}
            };
        }

        public Dictionary<ResourceEnum, int> GetResources()
        {
            return _resources;
        }
        
        public void AddPoints(int points)
        {
            _points += points;
        }
        
        public int GetPoints()
        {
            return _points;
        }

        public int GetResourcesCount()
        {
            return _resources.Values.Sum();
        }
        
        public int GetDevelopmentCardsCount()
        {
            return _developmentCards.Count;
        }

        public void AddResource(ResourceEnum resource, int amount)
        {
            if (resource == ResourceEnum.NONE) return;
            _resources[resource] += amount;
        }

        private void RemoveResource(ResourceEnum resource, int amount)
        {
            if (resource == ResourceEnum.NONE) return;
            _resources[resource] -= amount;
        }

        public void DeductResourceCost(Dictionary<ResourceEnum, int> resourceCost)
        {
            foreach (var item in resourceCost)
            {
                RemoveResource(item.Key, item.Value);
            }
        }

        public bool CanAffordResource(Dictionary<ResourceEnum, int> resourceCost)
        {
            return resourceCost.All(item => _resources[item.Key] >= item.Value);
        }
        
        public void AddDevelopmentCard(CardTypeEnum cardType)
        {
            _developmentCards.Add(cardType);
        }
    }
}