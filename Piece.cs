using System;

namespace BoardGame
{
    public class Piece
    {
        public Piece() { }
        public Seed Item { get; set; }

        public Piece(Seed item)
        {
            Item = item;
        }
    }
}
