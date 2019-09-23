using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Game
    {
        public Board Board { get; set; }
        public Color Turn { get; set; }

        public Game()
        {
            Board = new Board();
            Turn = Color.White;
        }

        public Game(Board board, Color turn)
        {
            Board = board;
            Turn = turn;
        }

        public bool IsOver()
        {
            return Board.WhitePieces == 0 || Board.BlackPieces == 0;
        }

        public bool Play(Position position, Move move)
        {
            if (!Board.Action(position, move, Turn))
            {
                return false;
            }
            Turn = Turn == Color.White ? Color.Black : Color.White;
            return true;
        }

        public Game Clone()
        {
            return new Game(Board.Clone(), Turn);
        }
    }
}
