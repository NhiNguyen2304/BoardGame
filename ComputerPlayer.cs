using System;

namespace BoardGame
{
    class ComputerPlayer : Player
    {
        private string name;
        public override string Name
        {
            get { return name; }
            set { name = "C1"; }
        }

        // Computer mode with random move
        public override void Move(object obj, out int row, out int col)
        {
            bool isFound = false;
            int rowTemp, colTemp;
            var rand = new Random();
            var board = obj as Board;
            row = -1;
            col = -1;

            while (isFound == false)
            {
                rowTemp = rand.Next(1, 4) - 1; // choose random row
                colTemp = rand.Next(1, 4) - 1; // choose random col
                if (board.cells[rowTemp, colTemp].content != Seed.CROSS 
                    && board.cells[rowTemp, colTemp].content != Seed.NOUGHT)
                {
                    isFound = true;
                    row = rowTemp; // Minus 1 because matrix count from 0
                    col = colTemp; // Minus 1 because matrix count from 0
                }
            }
        }
    }
}
