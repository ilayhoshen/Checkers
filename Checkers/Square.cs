namespace Checkers
{
    public class Square
    {
        public bool IsEmpty { get; set; }
        public Piece Piece { get; set; }

        public Square()
        {
            IsEmpty = true;
            Piece = null;
        }

        public Square(Color color, bool isKing)
        {
            IsEmpty = false;
            if (isKing)
            {
                Piece = new King(color);
            }
            else
            {
                Piece = new Soldier(color);
            }
        }
    }
}