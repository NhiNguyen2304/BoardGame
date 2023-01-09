using static System.Console;

namespace BoardGame
{
    public class Board
    {
        public int Row { get; set; }
        public int Col { get; set; }

        // A board can be any shape by Graphic.
        // This situation is square board with cells
        public Graphic[,] cells;
        private Cell Cell { get; set; }

        //private Piece piece;

        public Board(int row, int col, Cell cell)
        {
            Row = row;
            Col = col;
            Cell = cell;
            //InitialGame();
        }

        public Board(int row, int col, Cell[,] cells)
        {
            Row = row;
            Col = col;
            this.cells = cells;
            //InitialGame();
        }

        public Board()
        {
            // Generate board by InitialGame funciton
            //InitialGame();
        }

        /*public void InitialGame()
        {
            cells = new Cell[Row,Col];  // allocate cells
            for (int i = 0; i < Row; ++i)
            {
                for (int j = 0; j < Col; ++j)
                {
                    // Allocate element in array
                    cells[i, j] = new Cell(i, j);
                }
            }
        }*/

        // DrawBoard with vertical and horizontal
        public void DrawBoard(object piece)
        {
            WriteLine("");
            for (int i = 0; i < Row; ++i)
            {
                for (int j = 0; j < Col; ++j)
                {

                    Write(" ");
                    cells[i, j].Draw();
                    Write(" ");
                    if (j != Row - 1)
                    {
                        Write("|");   // print vertical
                    }
                }
                WriteLine("");
                if (i != Col - 1)
                {
                    WriteLine("-----------"); // print horizontal
                }
            }
            WriteLine();
        }
    }
}
