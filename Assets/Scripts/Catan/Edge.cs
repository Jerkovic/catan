namespace Catan
{
    public class Edge
    {
        private readonly Corner _left;
        private readonly Corner _right;
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
        
        public Corner GetLeftCorner()
        {
            return _left;
        }
        
        public Corner GetRightCorner()
        {
            return _right;
        }

        public override int GetHashCode()
        {
            return _left.GetHashCode() + _right.GetHashCode();
        }

        public override string ToString()
        {
            return "edge";
        }
    }
}