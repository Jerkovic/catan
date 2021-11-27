using System;

namespace Catan
{
    public class Corner
    {
        // owner, settlement status
        private readonly bool _port;
        private CornerStateEnum _state;
        private string _cornerPlayerId;
        
        public readonly int hex1;
        public readonly int hex2;
        public readonly int hex3;

        public Corner(int hex1, int hex2, int hex3, bool port = false)
        {
            _state = CornerStateEnum.EMPTY;
            this.hex1 = hex1;
            this.hex2 = hex2;
            this.hex3 = hex3;
            _port = port;
            _cornerPlayerId = null;
        }

        public bool PlaceSettlement(string playerId)
        {
            if (!string.IsNullOrEmpty(_cornerPlayerId)) return false;
            _state = CornerStateEnum.SETTLEMENT;
            _cornerPlayerId = playerId;
            return true;
        }

        public bool PlaceCity(string playerId)
        {
            if (!_cornerPlayerId.Equals(playerId)) return false;
            _state = CornerStateEnum.CITY;
            _cornerPlayerId = playerId;
            return true;
        }

        public CornerStateEnum GetState()
        {
            return _state;
        }

        public bool IsPort()
        {
            return _port;
        }

        public override int GetHashCode()
        {
            return hex1 + hex2 + hex3;
        }

        public override string ToString()
        {
            return hex1 + ", " + hex2 + ", " + hex3;
        }
    }
}