using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    public class Soldier : Piece
    {
        public Soldier(Color color) : base(color)
        {

        }

        public override List<Move> PossibleMoves(Board board, Position position)
        {
            var rowIncrease = Color == Color.White ? 1 : -1;
            var moves = new List<Move>();
            var leftStep = new Position(position.Row + rowIncrease, position.Column - 1);
            var rightStep = new Position(position.Row + rowIncrease, position.Column + 1);
            TryAddStep(board, moves, leftStep);
            TryAddStep(board, moves, rightStep);            
            moves.AddRange(BuildEatingMoves(board, position, -1, rowIncrease, new List<Position>()));
            moves.AddRange(BuildEatingMoves(board, position, 1, rowIncrease, new List<Position>()));

            return moves;
        }

        private void TryAddStep(Board board, List<Move> moves, Position step)
        {
            if (Board.CheckBounds(step))
            {
                var square = board[step];
                if (square.IsEmpty)
                {
                    var move = new Move();
                    move.Positions.Add(step);
                    moves.Add(move);
                }
            }
        }        

        private List<Move> BuildEatingMoves(Board board, Position position, int columnIncrease, int rowIncrease, List<Position> eatenPositions)
        {
            var moves = new List<Move>();

            var oneLeftStep = new Position(position.Row + rowIncrease, position.Column + columnIncrease);
            var twoLeftStep = new Position(position.Row + rowIncrease * 2, position.Column + columnIncrease * 2);
            if (Board.CheckBounds(twoLeftStep))
            {
                var midSquare = board[oneLeftStep];
                var square = board[twoLeftStep];
                if (square.IsEmpty && !midSquare.IsEmpty && midSquare.Piece.Color != Color && !eatenPositions.Any(p => p.Equals(oneLeftStep)))
                {
                    eatenPositions.Add(oneLeftStep);
                    var innerMoves = BuildEatingMoves(board, twoLeftStep, -1, rowIncrease, ClonePositions(eatenPositions));
                    innerMoves.AddRange(BuildEatingMoves(board, twoLeftStep, 1, rowIncrease, ClonePositions(eatenPositions)));
                    var move = new Move();
                    move.Positions.Add(twoLeftStep);
                    moves.Add(move);
                    foreach (var innerMove in innerMoves)
                    {
                        move = new Move();
                        move.Positions.Add(twoLeftStep);
                        move.Positions.AddRange(innerMove.Positions);
                        moves.Add(move);
                    }
                }
            }

            return moves;
        }

        private List<Position> ClonePositions(List<Position> positions)
        {
            return positions.Select(p => new Position(p.Row, p.Column)).ToList();
        }
    }
}
