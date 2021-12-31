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
                {ResourceEnum.WOOD, 0},
                {ResourceEnum.BRICK, 0},
                {ResourceEnum.SHEEP, 2},
                {ResourceEnum.WHEAT, 2},
                {ResourceEnum.ORE, 2}
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
        
        public void RemoveResource(ResourceEnum resource, int amount)
        {
            if (resource == ResourceEnum.NONE) return;
            _resources[resource] -= amount;
        }

        public bool CanAffordResource(Dictionary<ResourceEnum, int> resourceCost)
        {
            return Costs.DevCard.All(item => _resources[item.Key] >= item.Value);
        }
        
        public void AddDevelopmentCard(CardTypeEnum cardType)
        {
            _developmentCards.Add(cardType);
        }
    }
}