using System;

namespace BoardGame
{
    public class Player
    {
        public virtual string Name { get; set; }
        public int Turns { get; set; }
        public int Wins { get; set; }
        public string Char { get; set; }

        public Player()
        {
            this.Name = "";
            this.Char = "";
            this.Wins = 0;
            this.Turns = 0;
        }

        public virtual void SetName(string text)
        {
            // Set player name will be implemented in child classes
            throw new NotImplementedException("Player name should be input");
        }

        public virtual void Move(object obj, out int row, out int col)
        {
            throw new NotImplementedException("User Move should implement");
        }

        public void ResetPlayerData()
        {
            this.Name = "";
            this.Turns = 0;
            this.Wins = 0;
        }
    }
}
