using System.Collections.Generic;
using JetBrains.Annotations;

namespace Catan
{
    public class Edge
    {
        private readonly Corner _left;
        private readonly Corner _right;
        private bool _road;

        private string _edgePlayerGuid;

        public Edge(Corner left, Corner right)
        {
            this._left = left;
            this._right = right;
            this._road = false;
            _edgePlayerGuid = null;
        }

        [CanBeNull]
        public Corner GetAdjacentCorner(Corner corner)
        {
            if (corner.GetHashCode() == _left.GetHashCode()) return _right;
            if (corner.GetHashCode() == _right.GetHashCode()) return _left;
            return null;
        }

        public IEnumerable<Corner> GetCorners()
        {
            return new List<Corner>
            {
                _left,
                _right
            };
        }

        public bool HasRoad()
        {
            return _road;
        }
        
        public bool OwnedByPlayerGuid(string guid)
        {
            return _edgePlayerGuid != null && _edgePlayerGuid.Equals(guid);
        }
        
        public string GetPlayerGuid()
        {
            return _edgePlayerGuid;
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
            return "edge-" + GetHashCode();
        }

        public bool PlaceRoad(string playerId)
        {
            if (!string.IsNullOrEmpty(_edgePlayerGuid) || _road) return false;
            _road = true;
            _edgePlayerGuid = playerId;
            return true;
        }
    }
}