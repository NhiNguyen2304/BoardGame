using System;

namespace BoardGame
{
    public class HumanPlayer : Player
    {
        public override void SetName(string text)
        {
            while (text.Length < 1)
            {
                Console.WriteLine("Invalid Player name, try again: ");
                text = Console.ReadLine();
            }
            this.Name = text;
        }
    }
}
