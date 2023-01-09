using System;
using System.Collections.Generic;
using static System.Console;

namespace BoardGame
{

    // Template Design pattern?
    // A template of game that every inherited game should implement
    //
    // Abstract game
    public abstract class Game
    {
        protected Player player1;
        protected Player player2;
        protected Board board;
        protected Seed currentPlayer;
        protected GameState currentState;
        protected GameHistory history;
        protected MappingPiece mappingPiece;

        // generate steps for log file with format: Piece + : + row + col (Ex: Nought:22)
        protected List<string> steps = new List<string>();


        // This method input will return game with new or load game from current file
        // Return true: user input NEW game 
        // Return false: invalid input or QUIT 
        // Load game from file: user input LOAD
        protected string IntroduceMenu()
        {
            string input = null;
            WriteLine("Game Menu");
            WriteLine("Please enter");
            WriteLine("UNDO: undo your steps");
            WriteLine("REDO: redo undo step");
            WriteLine("QUIT: quit game");
            Write("Input here: ");
            input = ReadLine();
            return input;
        }

        // Handle input for new game or load latest game state
        protected int InputGameType()
        {
            string input = null;
            WriteLine("Please enter 'new' game if you want to start new game");
            WriteLine("Please enter 'load' game if you want to load your latest game");
            WriteLine("Please enter 'quit' if you want to exit");
            Write("Input here: ");

            while (input == null)
            {
                input = ReadLine();

                if (input.ToUpper() == "NEW")
                {
                    return 1;
                }
                if (input.ToUpper() == "LOAD")
                {
                    return 2;
                }

                if (input.ToUpper() == "QUIT")
                    return 0;

                WriteLine("Plese enter your input again we don't recognise your " + input);
                input = null;
            }
            return 0;
        }
        protected void InputGameMode()
        {
            int inputGameMode = 0;
            WriteLine("Please choose game mode (1 or 2): ");
            WriteLine("1. Computer vs Human");
            WriteLine("2. Human vs Human");
            Write("Input here: ");
        
            while (inputGameMode == 0)
            {
                inputGameMode = Int32.Parse(ReadLine());
                InitialPlayers(inputGameMode);
                WriteLine("\nInput MENU (see menu options) or\n" +
                "HELP to assist users with the available commands \n");
            }
        }
        protected void InitialPlayers(int inputGameMode)
        {
            if (inputGameMode == 2)
            {
                player1 = new HumanPlayer();
                player1.SetName("H1");
                player2 = new HumanPlayer();
                player2.SetName("H2");
                WriteLine(player1.Name + " fight " + player2.Name);
                return;
            }
            else
            {
                player1 = new HumanPlayer();
                player1.SetName("H1");
                player2 = new ComputerPlayer();
                WriteLine(player1.Name + " fight " + player2.Name);
                return;
            }
        }
        protected virtual bool InitialGame()
        {
            throw new NotImplementedException("InitialGame() should be implemented");
        }
        protected virtual void NewGame()
        {
            throw new NotImplementedException("NewGame() should be implemented");
        }
        public virtual void ControlGame()
        {
            throw new NotImplementedException("ControlGame() should be implemented");
        }
        protected virtual void UndoGame()
        {
            throw new NotImplementedException("UndoGame() should be implemented");
        }
        protected virtual void RedoGame()
        {
            throw new NotImplementedException("RedoGame() should be implemented");
        }

        protected void SaveGame(int playerMode)
        {
            GameLog.SaveCurrentGame("XOLog", steps, playerMode);
        }

        protected List<string> LoadGame()
        {
            List<string> steps = new List<string>();
            try
            {
                steps = GameLog.LoadSavedGame("XOLog");
            }
            catch (Exception)
            {

                Console.WriteLine("Sorry! You don't have any saved game");
            }
            
            return steps;
        }
    }
}
