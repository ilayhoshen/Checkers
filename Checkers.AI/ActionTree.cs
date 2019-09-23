using System.Collections.Generic;

namespace Checkers.AI
{
    public class ActionTree
    {
        public ActionTreeVertex Root { get; set; }

        public ActionTree(Game game)
        {
            Root = new ActionTreeVertex(game);
        }
    }

    public class ActionTreeVertex
    {
        public Game Game { get; set; }
        public int Score { get; set; }
        public Dictionary<MoveAction, ActionTreeVertex> Branches { get; set; }

        public ActionTreeVertex(Game game)
        {
            Game = game;
            Branches = new Dictionary<MoveAction, ActionTreeVertex>();
        }
    }
}
