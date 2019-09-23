using System;
using System.Linq;

namespace Checkers.AI
{
    public class EvaluationFunction
    {
        private int m_RemainingSoldiersMultiplier = 5;
        private int m_RemainingKingsMultiplier = 7;
        private int m_FreeWigsMultiplier = 1;
        private int m_CornersMultiplier = 2;

        public int Evaluate(Board board, Color color)
        {
            var rivalColor = color == Color.Black ? Color.White : Color.Black;

            var myRemainingPieces = RemainingSoldiers(board, color);
            var rivalRemainingPieces = RemainingSoldiers(board, rivalColor);

            var myRemainingKings = RemainingKings(board, color);
            var rivalRemainingKings = RemainingKings(board, rivalColor);

            var myFreeWigs = FreeWigs(board, color);
            var rivalFreeWigs = FreeWigs(board, rivalColor);

            var myCorners = Corners(board, color);
            var rivalCorners = Corners(board, rivalColor);
            
            return ((myRemainingPieces - rivalRemainingPieces) * m_RemainingSoldiersMultiplier) +
                   ((myRemainingKings - rivalRemainingKings) * m_RemainingSoldiersMultiplier) +
                   ((rivalFreeWigs - myFreeWigs) * m_FreeWigsMultiplier) + 
                   ((myCorners - rivalCorners) * m_CornersMultiplier);
        }

        private int RemainingSoldiers(Board board, Color color)
        {
            return board.Squares.Cast<Square>().Count(s => !s.IsEmpty && s.Piece.Color == color);
        }

        private int RemainingKings(Board board, Color color)
        {
            return board.Squares.Cast<Square>().Count(s => !s.IsEmpty && s.Piece.Color == color && s.Piece is King);
        }

        private int Corners(Board board, Color color)
        {
            var sum = 0;
            for (var i = 0; i < Board.ROW_COUNT; i++)
            {
                if (!board[i, 0].IsEmpty && board[i, 0].Piece.Color == color)
                {
                    sum++;
                }
                if (!board[i, Board.COLUMN_COUNT - 1].IsEmpty && board[i, Board.COLUMN_COUNT - 1].Piece.Color == color)
                {
                    sum++;
                }
                if (!board[0, i].IsEmpty && board[0, i].Piece.Color == color)
                {
                    sum++;
                }
                if (!board[Board.ROW_COUNT - 1, i].IsEmpty && board[Board.ROW_COUNT - 1, i].Piece.Color == color)
                {
                    sum++;
                }
            }

            return sum;
        }

        private int FreeWigs(Board board, Color color)
        {
            var sum = 0;
            for (var row = 0; row < Board.ROW_COUNT; row++)
            {
                for (var col = 0; col < Board.COLUMN_COUNT; col++)
                {
                    if (!board[row, col].IsEmpty && board[row, col].Piece.Color == color)
                    {
                        sum += PositionFreeWigs(board, color, row, col);
                    }
                }
            }

            return sum;
        }

        private int PositionFreeWigs(Board board, Color color, int row, int col)
        {
            var result = 0;
            if (row - 1 >= 0 && col - 1 >= 0 && board[row - 1, col - 1].IsEmpty)
            {
                result++;
            }
            if (row - 1 >= 0 && col + 1 < Board.COLUMN_COUNT && board[row - 1, col + 1].IsEmpty)
            {
                result++;
            }
            if (row + 1 < Board.ROW_COUNT && col - 1 >= 0 && board[row + 1, col - 1].IsEmpty)
            {
                result++;
            }
            if (row + 1 < Board.ROW_COUNT && col + 1 < Board.COLUMN_COUNT && board[row + 1, col + 1].IsEmpty)
            {
                result++;
            }
            return result;
        }
    }
}
