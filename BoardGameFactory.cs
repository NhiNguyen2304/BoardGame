using System;

namespace BoardGame
{
    // This is BoardGameFactory class which implements the AbstractFactory interface to create concrete products.
    public class BoardGameFactory : GameFactory
    {
        public Game GetGame(string game, int row, int col)
        {
            switch (game)
            {
                case "TicTacToe":
                    return new TicTacToeGame(row, col);
                case "Reversi":
                // Can new Reversi game class here
                default:
                    throw new ApplicationException(string.Format("Game '{0}' cannot be created", game));
            }
        }
    }
}
