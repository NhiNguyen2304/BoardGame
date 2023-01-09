using static System.Console;

namespace BoardGame
{
    public enum Seed
    {
        CROSS = 'X',
        NOUGHT = 'O',
        NO_SEED = ' '
    }

    // CompositeElement define neccesarry operations
    // Cell is using for square board
    public class Cell : Graphic
    {
        private int row;
        private int collumn;
        //public Seed content;

        public Cell() : base()
        {
        }

        public Cell(int row, int collumn)
        {
            this.row = row;
            this.collumn = collumn;
            this.content = Seed.NO_SEED; // inital cell with no seed
        }

        public override void Draw()
        {
            switch (this.content)
            {
                case Seed.CROSS:
                    Write("X"); break;
                case Seed.NOUGHT:
                    Write("O"); break;
                case Seed.NO_SEED:
                    Write(" "); break;
            }
        }
    }
}
