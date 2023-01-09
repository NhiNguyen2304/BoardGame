using System;

namespace BoardGame
{
    // This is an interface which is used to create abstract game
    public interface GameFactory
    {
        Game GetGame(string game, int row, int col);
    }
}
