namespace logic
{
    public class Catan
    {
        private readonly Board _board;
        
        public Catan()
        {
            _board = new Board(3);
        }

        public Board GetBoard()
        {
            return _board;
        }
        
    }
}