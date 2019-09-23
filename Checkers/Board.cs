using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    public class Board
    {
        public const int ROW_COUNT = 8;
        public const int COLUMN_COUNT = 8;
        public Square[,] Squares { get; set; }
        public int WhitePieces { get; set; }
        public int BlackPieces { get; set; }

        public Board()
        {
            InitBoard();
            WhitePieces = 12;
            BlackPieces = 12;
        }

        public Board(Square[,] squares)
        {
            Squares = squares;
            WhitePieces = Squares.Cast<Square>().Count(s => !s.IsEmpty && s.Piece.Color == Color.White);
            BlackPieces = Squares.Cast<Square>().Count(s => !s.IsEmpty && s.Piece.Color == Color.Black);
        }

        private void InitBoard()
        {
            Squares = new Square[ROW_COUNT, COLUMN_COUNT];
            for (var row = 0; row < Squares.GetLength(0); row++)
            {
                for (var col = 0; col < Squares.GetLength(1); col++)
                {
                    if (row >= 3 && row <= 4)
                    {
                        Squares[row, col] = new Square();
                    }
                    else
                    {
                        if (row % 2 == col % 2)
                        {
                            if (row < 3)
                            {
                                Squares[row, col] = new Square(Color.White, false);
                            }
                            else
                            {
                                Squares[row, col] = new Square(Color.Black, false);
                            }
                        }
                        else
                        {
                            Squares[row, col] = new Square();
                        }
                    }
                }
            }
        }

        public Square this[int row, int column]
        {
            get { return Squares[row, column]; }
            set { Squares[row, column] = value; }
        }

        public Square this[Position position]
        {
            get { return Squares[position.Row, position.Column]; }
            set { Squares[position.Row, position.Column] = value; }
        }

        public bool Action(Position position, Move move, Color turn)
        {
            if (!IsLegalAction(position, move, turn))
            {
                return false;
            }

            var currentPosition = position;
            foreach (var step in move.Positions)
            {
                DoStep(currentPosition, step, turn);
                currentPosition = step;
            }
            
            return true;
        }

        private void DoStep(Position position, Position step, Color turn)
        {
            if ((turn == Color.Black && step.Row == 0) || (turn == Color.White && step.Row == Board.ROW_COUNT - 1))
            {
                this[step] = new Square(turn, true);
            }
            else
            {
                this[step] = this[position];
            }

            this[position] = new Square();
            if (Math.Abs(position.Row - step.Row) > 1)
            {
                this[(position.Row + step.Row) / 2, (position.Column + step.Column) / 2] = new Square();
                if (turn == Color.Black)
                {
                    WhitePieces--;
                }
                else
                {
                    BlackPieces--;
                }
            }
        }

        private bool IsLegalAction(Position position, Move move, Color turn)
        {
            var square = this[position];
            if (square.IsEmpty)
            {
                return false;
            }

            if (square.Piece.Color != turn)
            {
                return false;
            }

            if (!square.Piece.PossibleMoves(this, position).Any(m => m.Equals(move)))
            {
                return false;
            }

            return true;
        }

        public List<Move> PossibleMoves(Position position)
        {
            return Squares[position.Row, position.Column].Piece.PossibleMoves(this, position);
        }

        public Board Clone()
        {
            var squaresCopy = new Square[ROW_COUNT, COLUMN_COUNT];
            for (var row = 0; row < ROW_COUNT; row++)
            {
                for (var col = 0; col < COLUMN_COUNT; col++)
                {
                    squaresCopy[row, col] = Squares[row, col];
                }
            }
            return new Board(squaresCopy);
        }

        public static bool CheckBounds(Position position)
        {
            return ROW_COUNT > position.Row && position.Row >= 0 && COLUMN_COUNT > position.Column &&
                   position.Column >= 0;
        }
    }
}