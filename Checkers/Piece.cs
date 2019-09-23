using System.Collections.Generic;

namespace Checkers
{
    public enum Color { White, Black }

    public abstract class Piece
    {
        public Color Color { get; set; }

        protected Piece(Color color)
        {
            Color = color;
        }

        public abstract List<Move> PossibleMoves(Board board, Position position);
    }
}