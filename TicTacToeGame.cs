using System;
using System.Collections.Generic;
using static System.Console;

namespace BoardGame
{
    public enum GameState
    {
        PLAYING = 0,
        DRAW = 1,
        CROSS_WON = 2,
        NOUGHT_WON = 3
    }

    // Tic Tac Toe Game is inherited from Game class. Tic tac toe is a kind of Game
    // Can create new board by inherited from Game class
    //
    // Concrete Game
    public class TicTacToeGame : Game
    {
        public int ROW { get; set; }
        public int COL { get; set; }

        // Player mode which input from user
        private const int COMPUTER_MODE = 1;
        private const int HUMAN_MODE = 2;

        // flag for load game
        
        private int playerMode = 0;
        private int countMove = 0;

        public TicTacToeGame()
        {
        }

        public TicTacToeGame(int row, int col)
        {
            this.ROW = row;
            this.COL = col;
        }

        // Initial new board with ROW and COL
        private void InitialBoard()
        {
            board.cells = new Cell[ROW, COL];  // allocate cells
            for (int i = 0; i < board.Row; ++i)
            {
                for (int j = 0; j < board.Row; ++j)
                {
                    // Allocate element in array
                    board.cells[i, j] = new Cell(i, j);
                }
            }
        }

        // Logic handle game with new game or load from lastest game state saving
        protected override bool InitialGame()
        {
            bool isLoadGame = false;
            Cell cell = new Cell(ROW, COL);
            board = new Board(ROW, COL, cell);
            InitialBoard();
            int checkGameType = 0;
            checkGameType = InputGameType();
            List<string> loadsteps = new List<string>();

            // checkGameType: NEW = 1, LOAD = 2, QUIT = 0
            if (checkGameType == 1)
            {
                InputGameMode();
                isLoadGame = true;
            }
            if (checkGameType == 2)
            {
                loadsteps = LoadGame();
                if (loadsteps != null && loadsteps.Count > 0)
                {
                    DrawboardFromLog(loadsteps);
                    isLoadGame = true;
                    InitialPlayers(playerMode);
                }
                
            }
            if (checkGameType == 0)
            {
                Write("Thank you!!!!");
                System.Environment.Exit(1); // Exit when quit
                ReadKey();
            }
            if (isLoadGame)
            {
                board.DrawBoard(currentPlayer);
            }
            return isLoadGame;
        }
        protected override void NewGame()
        {
            currentPlayer = Seed.CROSS;   // cross plays first
            currentState = (int)GameState.PLAYING; // playing state for ready to play
            countMove = 0;
            //board.DrawBoard(currentPlayer);
        }

        public void NewGameFromLog()
        {
            currentState = (int)GameState.PLAYING; // playing state for ready to play
            //board.DrawBoard(currentPlayer);
        }

        // Switch players after a turn
        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == Seed.CROSS) ? Seed.NOUGHT : Seed.CROSS;
        }

        // Logic to handle user input
        private void InputPiece(out int row, out int col, int playerMode)
        {
            string numberString = "";
            string saveString = "";
            string menuString = "";
            int number = 0;
            int rowTemp = -1, colTemp = -1;
            bool invalidInput = false;
            List<string> loadsteps = new List<string>();
            do
            {
                if (currentPlayer == Seed.CROSS)
                {
                    Write("Player 'X', enter your move(row[1-3] column[1-3]) or MENU or HELP for advance tasks: ");
                }
                if (currentPlayer == Seed.NOUGHT)
                {
                    Write("Player 'O', enter your move(row[1-3] column[1-3]) or MENU or HELP for advance tasks: ");
                }
                numberString = ReadLine();

                // Logic handle Menu action
                if (numberString.ToUpper() == "MENU")
                {
                    menuString = IntroduceMenu();
                    // undo action
                    if (menuString.ToUpper() == "UNDO")
                    {
                        if (countMove > 1)
                        {
                            UndoGame();
                            invalidInput = false;
                            SwitchPlayer();
                        }
                        else
                        {
                            WriteLine("Undo and redo operations are not available until new moves have been made.");
                        }

                    }
                    // redo action
                    if (menuString.ToUpper() == "REDO")
                    {
                        if (countMove > 1)
                        {
                            RedoGame();
                            invalidInput = false;
                            SwitchPlayer();
                        }
                        else
                        {
                            WriteLine("Undo and redo operations are not available until new moves have been made.");
                        }

                    }

                    // quit action by save game option or don't save game
                    if (menuString.ToUpper() == "QUIT")
                    {
                        Write("Do you want to save this game (yes - save current game/no - quit without save): ");
                        saveString = ReadLine();
                        if (saveString.ToUpper() == "YES")
                        {
                            SaveGame(playerMode);
                            Write("Thank you!!!!");
                            System.Environment.Exit(1); // Exit when quit
                            ReadKey();
                        }
                        else
                        {
                            Write("Thank you!!!!");
                            System.Environment.Exit(1); // Exit when quit
                            ReadKey();
                        }

                    }
                }
                // help action to call PrimitiveOnlineHelp
                if (numberString.ToUpper() == "HELP")
                {
                    PrimitiveOnlineHelp.CallOnlineHelp();
                }
                // Load current game from save file
                if (numberString.ToUpper() == "LOAD")
                {
                    loadsteps = LoadGame();
                    if(loadsteps != null && loadsteps.Count > 0)
                    {
                        DrawboardFromLog(steps);
                        invalidInput = true;
                    }
                    
                }
                if (int.TryParse(numberString, out number))
                {
                    //int.TryParse(numberString, out number);
                    //input = Int32.Parse(inputTemp);

                    rowTemp = (number / 10) - 1;  // array index starts at 0 instead of 1
                    colTemp = (number % 10) - 1;
                    invalidInput = true;
                    countMove++;
                }

            } while (!invalidInput);
            
            row = rowTemp;
            col = colTemp;
        }
        // This function handle logic for undo action
        protected override void UndoGame()
        {
            // For Computer player. When undo happen should remove both piece from human and computer
            if (this.player2.Name == "C1")
            {
                // Pop an item in listMove to list of Undo action
                history.listMoveUndo.Push(history.listMove.Pop());
                steps.RemoveAt(steps.Count - 1);
            }
            history.listMoveUndo.Push(history.listMove.Pop());

            // Remove piece from save game list because of undo action
            steps.RemoveAt(steps.Count - 1);
            Cell cell = new Cell(ROW, COL);
            board = new Board(ROW, COL, cell);
            InitialBoard();
            int tempString;
            foreach (MappingPiece item in history.listMove)
            {
                currentPlayer = item.Piece;
                tempString = Int32.Parse(item.Position);
                int rowTemp = tempString / 10;
                int colTemp = tempString % 10;
                board.cells[rowTemp, colTemp].content = currentPlayer;
            }
            board.DrawBoard(currentPlayer);
        }
        // Logic handle Redo input
        protected override void RedoGame()
        {
            // For Computer player. When undo/redo should remove both piece from human and computer
            if (this.player2.Name == "C1")
            {
                history.listMove.Push(history.listMoveUndo.Pop());
            }
            // push back the last undo action into current list of moves
            history.listMove.Push(history.listMoveUndo.Pop());
            int tempString;
            foreach (MappingPiece item in history.listMove)
            {
                currentPlayer = item.Piece;
                tempString = Int32.Parse(item.Position);
                int rowTemp = tempString / 10;
                int colTemp = tempString % 10;
                board.cells[rowTemp, colTemp].content = currentPlayer;
            }
            board.DrawBoard(currentPlayer);
        }

        // Handle Draw board from LOG file (user want to save game state)
        private void DrawboardFromLog(List<string> steps)
        {
            string player;
            string position;
            int tempString;
            int rowTemp = -1;
            int colTemp = -1;
            if (steps != null && steps.Count > 0)
            {
                playerMode = Int32.Parse(steps[0]);
                steps.RemoveAt(0);
                foreach (var text in steps)
                {
                    if (!String.IsNullOrWhiteSpace(text))
                    {
                        // Add move in log file to current steps in game
                        // Change
                        this.steps.Add(text);

                        int charLocation = text.IndexOf(":", StringComparison.Ordinal);
                        int length = text.Length;

                        if (charLocation > 0)
                        {
                            player = text.Substring(0, charLocation);
                            position = text.Substring(charLocation + 1, 2);
                            tempString = Int32.Parse(position);
                            rowTemp = tempString / 10;
                            colTemp = tempString % 10;

                            switch (player)
                            {
                                case "CROSS":
                                    currentPlayer = Seed.CROSS; break;
                                case "NOUGHT":
                                    currentPlayer = Seed.NOUGHT; break;
                                default:
                                    currentPlayer = Seed.NO_SEED; break;
                            }
                        }
                        board.cells[rowTemp, colTemp].content = currentPlayer;

                        // Change
                        mappingPiece = new MappingPiece(currentPlayer, rowTemp.ToString() + colTemp.ToString());
                        history.listMove.Push((MappingPiece)mappingPiece);
                    }
                }
            }
        }

        // Function to handle GameState: Playing, Draw or Win
        private GameState CheckGameState(Seed player, int selectedRow, int selectedCol)
        {
            // Update game board
            board.cells[selectedRow, selectedCol].content = player;

            // Compute and return the new game state
            if (board.cells[selectedRow, 0].content == player  // 3-in-the-row
                      && board.cells[selectedRow, 1].content == player
                      && board.cells[selectedRow, 2].content == player
                   || board.cells[0, selectedCol].content == player // 3-in-the-column
                      && board.cells[1, selectedCol].content == player
                      && board.cells[2, selectedCol].content == player
                   || selectedRow == selectedCol         // 3-in-the-diagonal
                      && board.cells[0, 0].content == player
                      && board.cells[1, 1].content == player
                      && board.cells[2, 2].content == player
                   || selectedRow + selectedCol == 2     // 3-in-the-opposite-diagonal
                      && board.cells[0, 2].content == player
                      && board.cells[1, 1].content == player
                      && board.cells[2, 0].content == player)
            {
                return (player == Seed.CROSS) ? GameState.CROSS_WON : GameState.NOUGHT_WON;
            }
            else
            {
                // No one win. Check for DRAW (all cells) or PLAYING.
                for (int row = 0; row < ROW; ++row)
                {
                    for (int col = 0; col < COL; ++col)
                    {
                        if (board.cells[row, col].content == Seed.NO_SEED)
                        {
                            return GameState.PLAYING; // still have empty cells
                        }
                    }
                }
                return GameState.DRAW; // no empty cell, it's a draw
            }
        }

        // Function control game with different mode (Human vs Human, Computer vs Human)
        public void PlayGame(int playerMode)
        {
            bool validInput = false;  // for input validation
            int inputRow, inputCol;
            do
            {
                if (playerMode == COMPUTER_MODE)  // Handle Computer mode with Human logic
                {
                    if (currentPlayer == Seed.CROSS) // Computer turn. Default computer player is CROSS PIECE
                    {
                        player2.Move(board, out inputRow, out inputCol);
                        WriteLine("Player Computer 'X', enter their move(row[1-3] column[1-3]): {0}{1}"
                            , inputRow + 1, inputCol + 1); // Plus + for displaying, because matrix count from index 0
                        playerMode++; // temporary plus 1 to use user turn
                        countMove++;
                    }
                    else // Human turn
                    {
                        InputPiece(out inputRow, out inputCol, playerMode);
                        playerMode--; // if playerMode = 2 => Minus 1 to get back computer turn
                        countMove++;
                    }
                }
                else // Handle Human Human mode logic
                {
                    InputPiece(out inputRow, out inputCol, playerMode);
                    playerMode++;
                    countMove++;

                }


                if (inputRow >= 0 && inputRow < ROW && inputCol >= 0 && inputCol < COL
                             && board.cells[inputRow, inputCol].content == Seed.NO_SEED)
                {
                    // Update board and return the new game state after user move
                    currentState = CheckGameState(currentPlayer, inputRow, inputCol);
                    validInput = true;  // input okay, exit loop

                    mappingPiece = new MappingPiece(currentPlayer, inputRow.ToString() + inputCol.ToString());
                    history.listMove.Push((MappingPiece)mappingPiece);

                    // generate steps for log file with format: Piece+ row + col
                    steps.Add(currentPlayer + ":" + inputRow.ToString() + "" + inputCol.ToString());
                    //listTemp.Add(mappingPiece);
                }
                else
                {
                    WriteLine("This move: out of range 3x3 or already taken. Input Menu or Help for more information. Please try again!!");
                    PrimitiveOnlineHelp.InvalidMoveHelp();
                    playerMode--; // Get Back to computer mode if invalid move (if in computer mode)
                }
            } while (!validInput);  // try again if invalid input
        }

        // function handle control game flow
        public override void ControlGame()
        {
            //int playerMode = 0;
            bool isLoadGame = false;
            history = new GameHistory();
            history.GameName = "Tic Tac Toe";
            // Input until input valid value
            do
            {
                isLoadGame = InitialGame();
            } while (!isLoadGame);
            NewGame();

            // Loop until GameState is not Playing
            do
            {
                // Compute: userTurn = 1, Human: userTurn = 2
                if (this.player2.Name == "C1")
                {
                    playerMode = COMPUTER_MODE;
                }
                else
                {
                    playerMode = HUMAN_MODE;
                }
                PlayGame(playerMode);
                board.DrawBoard(currentPlayer);

                // Message for game
                PrintWinner();
                // Switch currentPlayer
                SwitchPlayer();

            } while (currentState == GameState.PLAYING);
/*            else
            {
                InitialPlayers(playerMode);
                do
                {
                    PlayGame(playerMode);
                    board.DrawBoard(currentPlayer);

                    // Message for game
                    PrintWinner();
                    // Switch currentPlayer
                    SwitchPlayer();
                } while (currentState == GameState.PLAYING);
               
            }*/
        }
        private void PrintWinner()
        {
            if (currentState == GameState.CROSS_WON)
            {
                WriteLine("'X' won!\n Thank you for good game!");
            }
            else if (currentState == GameState.NOUGHT_WON)
            {
                WriteLine("'O' won!\n Thank you for good game!");
            }
            else if (currentState == GameState.DRAW)
            {
                WriteLine("It's Draw!\n Try Again!");
            }
        }
    }
}
