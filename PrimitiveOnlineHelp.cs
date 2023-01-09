using System;

namespace BoardGame
{
    // Only one instance of PrimitiveOnlineHelp is created in a game
    // TODO: should suggest user invalid move: invalid move will call this class
    public sealed class PrimitiveOnlineHelp
    {
        private static PrimitiveOnlineHelp _instance;
        private static string humanValidMove
            = "You place your piece follow with rule row[1-3] column[1-3]), \n" +
            "For example: 11 (1st row and 1st collumn)\n" +
            "Select   MENU then choose: \n" +
            "" +
            "\t UNDO: to undo previous move (with Human x Human mode), \n" +
            "              2 previous moves with (Computer x Human mode) \n" +
            "\t REDO: redo your steps \n" +
            "\t QUIT: quit game with save game options \n" +
            "\t \t save game (YES): save current game state for loading current game later \n" + 
            "\t \t not save game (NO) \n";
        private static string introduce = "Tic-tac-toe is played by two players who alternately place \n" +
            "the pieces X and O in one of the nine places on a three-by-three grid.\n" +
            "Size board: 3x3 \n" +
            "Win: The first player to make three consecutive rows wins!";
        private static string invalidMoveHelp =
            "You can place your piece (X or O) on board that doesn't have any other pieces.\n" +
            "You place your piece follow with rule row[1-3] column[1-3]) \n" +
            "For example: 11 (1st row and 1st collumn)\n";


        private PrimitiveOnlineHelp()
        {
        }
        public static void CallOnlineHelp()
        {
            // control only initial 1 instance through program
            if (_instance == null)
            {
                _instance = new PrimitiveOnlineHelp();
            }
            Console.WriteLine("**************************************************************************************");
            Console.WriteLine(introduce);
            Console.WriteLine("**************************************************************************************");
            Console.WriteLine(humanValidMove);
            Console.WriteLine("**************************************************************************************");
        }
        public static void InvalidMoveHelp()
        {
            // control only initial 1 instance through program
            if (_instance == null)
            {
                _instance = new PrimitiveOnlineHelp();
            }
            Console.WriteLine("**************************************************************************************");
            Console.WriteLine(invalidMoveHelp);
        }

    }
}
