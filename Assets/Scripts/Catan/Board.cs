using System.Collections.Generic;
using System.Linq;
using EventSystem;

namespace Catan
{
    public class Board
    {
        private readonly int _radius;
        private List<HexTile> _tiles;
        private List<Corner> _corners;
        private List<Edge> _edges;

        private HexTile _robberTile;

        public Board(int radius)
        {
            _radius = radius;
            GenerateTiles();
            GenerateCorners();
            GenerateEdges();
        }

        private void GenerateTiles()
        {
            var center = new CubicHexCoord(0, 0, 0);
            var board = CubicHexCoord.SpiralOutward(new CubicHexCoord(0, 0, 0), _radius - 1);
            var water = CubicHexCoord.Ring(center, _radius);

            var tp = new TileTypes();
            var chits = new Chits();

            _tiles = new List<HexTile>();

            foreach (var coordinate in board)
            {
                var type = (TileTypeEnum) tp.RandomNextTile();
                var chit = 0;
                if (type != TileTypeEnum.DESERT)
                {
                    chit = chits.RandomNextChit();
                }
                _tiles.Add(new HexTile(coordinate, type, chit));
            }

            foreach (var coordinate in water)
            {
                _tiles.Add(new HexTile(coordinate, TileTypeEnum.SEA, 0));
            }
        }

        public void SetRobberDesert()
        {
            var desertTile = _tiles.Single(tile => tile.GetTileType() == TileTypeEnum.DESERT);
            _robberTile = desertTile;
            Events.OnRobberMove.Invoke(desertTile);
        }

        public List<HexTile> GetTiles()
        {
            return _tiles;
        }

        public List<Corner> GetCorners()
        {
            return _corners;
        }

        public List<Edge> GetEdges()
        {
            return _edges;
        }

        private void GenerateEdges()
        {
            // An edge connects to corners. Total edges #72
            _edges = new List<Edge>();

            AddEdge(621001, 619967);
            AddEdge(621001, 621485);
            AddEdge(621001, 621551);
            AddEdge(619967, 619417);
            AddEdge(619967, 619483);
            AddEdge(619483, 618449);
            AddEdge(619483, 620033);
            AddEdge(620033, 619549);
            AddEdge(620033, 621067);
            AddEdge(621067, 621617);
            AddEdge(621067, 621551);
            AddEdge(621551, 622585);
            AddEdge(622585, 623135);
            AddEdge(622585, 623069);
            AddEdge(623069, 624103);
            AddEdge(623069, 622519);
            AddEdge(622519, 623003);
            AddEdge(622519, 621485);
            AddEdge(621485, 620935);
            AddEdge(620935, 621419);
            AddEdge(620935, 619901);
            AddEdge(619901, 619351);
            AddEdge(619901, 619417);
            AddEdge(619417, 618383);
            AddEdge(618383, 617833);
            AddEdge(618383, 617899);
            AddEdge(617899, 616865);
            AddEdge(617899, 618449);
            AddEdge(618449, 617965);
            AddEdge(617965, 616931);
            AddEdge(617965, 618515);
            AddEdge(618515, 618031);
            AddEdge(618515, 619549);
            AddEdge(619549, 620099);
            AddEdge(620099, 619615);
            AddEdge(620099, 621133);
            AddEdge(621133, 621683);
            AddEdge(621133, 621617);
            AddEdge(621617, 622651);
            AddEdge(622651, 623201);
            AddEdge(622651, 623135);
            AddEdge(623135, 624169);
            AddEdge(624169, 624719);
            AddEdge(624169, 624653);
            AddEdge(624653, 624103);
            AddEdge(624103, 624587);
            AddEdge(624587, 624037);
            AddEdge(624037, 623003);
            AddEdge(623003, 622453);
            AddEdge(622453, 621419);
            AddEdge(621419, 620869);
            AddEdge(620869, 619835);
            AddEdge(619835, 619351);
            AddEdge(619351, 618317);
            AddEdge(618317, 617833);
            AddEdge(617833, 616799);
            AddEdge(616799, 616315);
            AddEdge(616315, 616865);
            AddEdge(616865, 616381);
            AddEdge(616381, 616931);
            AddEdge(616931, 616447);
            AddEdge(616447, 616997);
            AddEdge(616997, 618031);
            AddEdge(618031, 618581);
            AddEdge(618581, 619615);
            AddEdge(619615, 620165);
            AddEdge(620165, 621199);
            AddEdge(621199, 621683);
            AddEdge(621683, 622717);
            AddEdge(622717, 623201);
            AddEdge(623201, 624235);
            AddEdge(624235, 624719);
        }

        private void GenerateCorners()
        {
            _corners = new List<Corner>();
            // corner placement center tile 
            AddCorner(206839, 207345, 206817);
            AddCorner(206839, 206817, 206311);
            AddCorner(206839, 206311, 206333);
            AddCorner(206839, 206333, 206861);
            AddCorner(206839, 206861, 207367);
            AddCorner(206839, 207367, 207345);

            // next ring total #18
            AddCorner(206817, 207345, 207323);
            AddCorner(206817, 207323, 206795);
            AddCorner(206817, 206795, 206289);
            AddCorner(206817, 206289, 206311);
            AddCorner(206311, 206289, 205783);
            AddCorner(206311, 205783, 205805);
            AddCorner(206311, 206333, 205805);
            AddCorner(206333, 205805, 205827);
            AddCorner(206333, 205827, 206355);
            AddCorner(206333, 206355, 206861);
            AddCorner(206861, 206355, 206883);
            AddCorner(206861, 206883, 207389);
            AddCorner(206861, 207367, 207389);
            AddCorner(207367, 207389, 207895);
            AddCorner(207367, 207895, 207873);
            AddCorner(207367, 207873, 207345);
            AddCorner(207345, 207873, 207851);
            AddCorner(207345, 207851, 207323);
            // End 2:nd ring  

            // Start 3 ring # of corners 30 here the port corners start to pop up
            AddCorner(206795, 207323, 207301, true);
            AddCorner(206795, 207301, 206773);
            AddCorner(206267, 206795, 206773);
            AddCorner(206289, 206795, 206267, true);
            AddCorner(205761, 206289, 206267, true);
            AddCorner(205783, 205761, 206289);
            AddCorner(205255, 205783, 205761, true);
            AddCorner(205277, 205783, 205255, true);
            AddCorner(205805, 205277, 205783);
            AddCorner(205299, 205805, 205277, true);
            AddCorner(205299, 205805, 205827, true);
            AddCorner(205321, 205827, 205299);
            AddCorner(205321, 205827, 205849);
            AddCorner(206355, 205827, 205849, true);
            AddCorner(206355, 205849, 206377, true);
            AddCorner(206355, 206377, 206883);
            AddCorner(206883, 206377, 206905, true);
            AddCorner(206883, 207411, 206905, true);
            AddCorner(206883, 207411, 207389);
            AddCorner(207389, 207917, 207411, true);
            AddCorner(207389, 207917, 207895, true);
            AddCorner(207895, 208423, 207917);
            AddCorner(207895, 208423, 208401);
            AddCorner(207895, 207873, 208401, true);
            AddCorner(207873, 208401, 208379, true);
            AddCorner(207873, 207851, 208379);
            AddCorner(207851, 208379, 208357, true);
            AddCorner(207851, 208357, 207829, true);
            AddCorner(207323, 207851, 207829);
            AddCorner(207323, 207829, 207301, true);
        }

        private void AddCorner(int hex1, int hex2, int hex3, bool port = false)
        {
            _corners.Add(new Corner(hex1, hex2, hex3, port));
        }

        private void AddEdge(int cornerHash1, int cornerHash2)
        {
            var corner1 = GetCornerByHashCode(cornerHash1);
            var corner2 = GetCornerByHashCode(cornerHash2);
            _edges.Add(new Edge(corner1, corner2));
        }

        private Edge GetEdgeByHashCode(int hashCode)
        {
            return _edges.Single(edge => edge.GetHashCode() == hashCode);
        }

        private Corner GetCornerByHashCode(int hashCode)
        {
            return _corners.Single(corner => corner.GetHashCode() == hashCode);
        }

        public HexTile GetTileByHashCode(int hashCode)
        {
            return _tiles.Single(tile => tile.GetHashCode() == hashCode);
        }
    }
}