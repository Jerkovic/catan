using System.Collections.Generic;
using Catan.Resources;

namespace Catan
{
    public static class Costs
    {
        public static readonly Dictionary<ResourceEnum, int> Road = new Dictionary<ResourceEnum, int>
        {
            {ResourceEnum.WOOD, 1},
            {ResourceEnum.BRICK, 1},
            {ResourceEnum.SHEEP, 0},
            {ResourceEnum.WHEAT, 0},
            {ResourceEnum.ORE, 0}
        };
        
        public static readonly Dictionary<ResourceEnum, int> Settlement = new Dictionary<ResourceEnum, int>
        {
            {ResourceEnum.WOOD, 1},
            {ResourceEnum.BRICK, 1},
            {ResourceEnum.SHEEP, 1},
            {ResourceEnum.WHEAT, 1},
            {ResourceEnum.ORE, 0}
        };
        
        public static readonly Dictionary<ResourceEnum, int> City = new Dictionary<ResourceEnum, int>
        {
            {ResourceEnum.WOOD, 0},
            {ResourceEnum.BRICK, 0},
            {ResourceEnum.SHEEP, 0},
            {ResourceEnum.WHEAT, 2},
            {ResourceEnum.ORE, 3}
        };
        
        public static readonly Dictionary<ResourceEnum, int> DevCard = new Dictionary<ResourceEnum, int>
        {
            {ResourceEnum.WOOD, 0},
            {ResourceEnum.BRICK, 0},
            {ResourceEnum.SHEEP, 1},
            {ResourceEnum.WHEAT, 1},
            {ResourceEnum.ORE, 1}
        };
    }
}