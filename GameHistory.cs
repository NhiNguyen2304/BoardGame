using System.Collections.Generic;

namespace BoardGame
{
    public class GameHistory
    {
        // listMove stores user moves.
        // Use stack to follow rule first in last out. Undo the lastest move
        public Stack<MappingPiece> listMove = new Stack<MappingPiece>();

        // listMoveUndo to save undo move and use for redo action
        // Use stack to follow rule first in last out. Redo the lastest undo action
        public Stack<MappingPiece> listMoveUndo = new Stack<MappingPiece>();
        public Player winner;
        public string GameName { get; set; }

        public GameHistory()
        {
        }

        public GameHistory(Player winner, string gameName)
        {
            this.winner = winner;
            this.GameName = gameName;
        }

        public GameHistory(Stack<MappingPiece> listMove, Stack<MappingPiece> listMoveUndo)
        {
            this.listMove = listMove;
            this.listMoveUndo = listMoveUndo;
        }
    }
    public class MappingPiece
    {
        public Seed Piece { get; set; }
        public string Position { get; set; }

        public MappingPiece()
        {
        }

        public MappingPiece(Seed piece, string position)
        {
            Piece = piece;
            Position = position;
        }
    }
}
