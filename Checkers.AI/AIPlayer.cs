using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.AI
{
    public class AIPlayer
    {
        public Color Color { get; set; }
        public EvaluationFunction Evaluation { get; set; }
        private Random m_Random = new Random();
        public AIPlayer(Color color, EvaluationFunction evaluation)
        {
            Color = color;
            Evaluation = evaluation;
        }

        public MoveAction Play(Game game)
        {
            var tree = BuildActionTree(game);
            Minimax(tree.Root);
            var max = tree.Root.Branches.Max(vertex => vertex.Value.Score);
            var topBranches = tree.Root.Branches.Where(vertex => vertex.Value.Score == max).ToList();
            return topBranches[m_Random.Next(0, topBranches.Count)].Key;
        }

        private void Minimax(ActionTreeVertex vertex)
        {
            foreach (var actionTreeVertex in vertex.Branches)
            {
                var min = actionTreeVertex.Value.Branches.Min(g => g.Value.Score);
                actionTreeVertex.Value.Score = min;
            }
        }

        private ActionTree BuildActionTree(Game game)
        {
            var tree = new ActionTree(game);
            tree.Root.Score = Evaluation.Evaluate(game.Board, Color);
            ExpandTree(tree.Root, 2);
            return tree;
        }        

        private void ExpandTree(ActionTreeVertex vertex, int level)
        {
            if (level > 0)
            {
                ExpandVertex(vertex);            
                foreach (var actionTreeVertex in vertex.Branches)
                {
                    ExpandTree(actionTreeVertex.Value, level - 1);
                }
            }
        }

        private void ExpandVertex(ActionTreeVertex vertex)
        {
            var actions = AllActions(vertex.Game.Board, vertex.Game.Turn);
            foreach (var action in actions)
            {
                var game = vertex.Game.Clone();
                game.Play(action.PositionToMove, action.Move);
                var innerVertex = new ActionTreeVertex(game);
                innerVertex.Score = Evaluation.Evaluate(game.Board, Color);
                vertex.Branches[action] = innerVertex;
            }
        }

        private List<MoveAction> AllActions(Board board, Color color)
        {
            var actions = new List<MoveAction>();
            for (var row = 0; row < Board.ROW_COUNT; row++)
            {
                for (var col = 0; col < Board.COLUMN_COUNT; col++)
                {
                    var position = new Position(row, col);
                    var square = board[position];
                    if (!square.IsEmpty && square.Piece.Color == color)
                    {
                        var possibleMoves = square.Piece.PossibleMoves(board, position);
                        actions.AddRange(possibleMoves.Select(m => new MoveAction() { PositionToMove = position, Move = m }).ToList());
                    }
                }
            }
            return actions;
        }
    }
}
