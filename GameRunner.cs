using System;

namespace BoardGame
{
    class GameRunner
    {
        static void Main(string[] args)
        {
             int row = 3;
             int col = 3;
             /*Cell cell = new Cell(row, col);
             Board board = new Board(row, col, cell);*/

             string gameName = "TicTacToe";
             GameFactory tictactoe = new BoardGameFactory(); ;
             Game game = tictactoe.GetGame(gameName, row, col);
             game.ControlGame();
        }
    }
}

