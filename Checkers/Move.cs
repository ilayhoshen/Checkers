using System.Collections.Generic;

namespace Checkers
{
    public class Move
    {
        public List<Position> Positions { get; set; }

        public Move()
        {
            Positions = new List<Position>();    
        }

        public bool Equals(Move move)
        {
            for (var i = 0; i < Positions.Count; i++)
            {
                if (move.Positions.Count <= i || move.Positions[i].Equals(Positions[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}