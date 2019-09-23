using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.AI;
using Microsoft.Win32;

namespace Checkers.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var aiPlayer = new AIPlayer(Color.Black, new EvaluationFunction());

            while (true)
            {
                PrintBoard(game.Board);

                System.Console.WriteLine();

                if (game.Turn == Color.White)
                {
                    System.Console.WriteLine(game.Turn + "'s turn. Please select position to move. e.g. '5,2'");
                    var piecePosition = System.Console.ReadLine().Split(',');
                    var position = new Position(int.Parse(piecePosition[0]), int.Parse(piecePosition[1]));
                    var moves = game.Board.PossibleMoves(position);


                    System.Console.WriteLine("Possible moves: ");
                    for (var i = 0; i < moves.Count; i++)
                    {
                        System.Console.Write(i + ". ");
                        PrintMove(position, moves[i]);
                    }

                    System.Console.WriteLine("Choose move. -1 to back");
                    var move = int.Parse(System.Console.ReadLine());
                    if (move != -1)
                    {
                        game.Play(position, moves[move]);
                    }
                }
                else
                {
                    System.Console.WriteLine("Click enter for AIPlayer to play");
                    System.Console.ReadLine();
                    var move = aiPlayer.Play(game);
                    System.Console.WriteLine("AI's move:");
                    PrintMove(move.PositionToMove, move.Move);
                    game.Play(move.PositionToMove, move.Move);
                    System.Console.ReadLine();
                }

                System.Console.Clear();

            }
        }

        private static void PrintMove(Position position, Move move)
        {
            System.Console.Write(String.Format("({0}, {1})", position.Row, position.Column));
            foreach (var step in move.Positions)
            {
                System.Console.Write(String.Format(" --> ({0}, {1})", step.Row, step.Column));
            }

            System.Console.WriteLine();
        }

        private static void PrintBoard(Board board)
        {
            System.Console.WriteLine("   0  1  2  3  4  5  6  7");
            for (var row = 0; row < board.Squares.GetLength(0); row++)
            {
                System.Console.Write(row + " ");
                for (var col = 0; col < board.Squares.GetLength(1); col++)
                {
                    var square = board[row, col];
                    if (square.IsEmpty)
                    {
                        System.Console.Write(" - ");
                    } else if (square.Piece.Color == Color.White)
                    {
                        System.Console.Write(" O ");
                    }
                    else
                    {
                        System.Console.Write(" X ");
                    }
                }

                System.Console.WriteLine();
            }
        }
    }
}
