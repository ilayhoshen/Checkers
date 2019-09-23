namespace Checkers
{
    public class Square
    {
        public bool IsEmpty { get; set; }
        public Soldier Piece { get; set; }

        public Square()
        {
            IsEmpty = true;
            Piece = null;
        }

        public Square(Color color)
        {
            IsEmpty = false;
            Piece = new Soldier(color);
        }
    }
}