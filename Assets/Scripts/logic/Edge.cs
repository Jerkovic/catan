namespace logic
{
    public class Edge
    {
        private Corner _left;
        private Corner _right;
        private readonly bool _road;
        
        public Edge(Corner left, Corner right)
        {
            this._left = left;
            this._right = right;
            this._road = false;
        }

        public bool HasRoad() 
        {
            return _road;
        }
    }
}