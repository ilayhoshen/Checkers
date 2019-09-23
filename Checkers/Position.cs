namespace Checkers
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(Position position)
        {
            return Row == position.Row && Column == position.Column;
        }
    }
}