using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;

namespace BoardGame
{
    // GameLog apply singleton design pattern
    public sealed class GameLog
    {
        // The GameLog's private constructor prevent new
        private GameLog() { }

        private static GameLog _instance;

        public static GameLog GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameLog();
            }
            return _instance;
        }
        // Argument
        // playerMode: Computer x Human (1), Human x Human (2)
        // Think about should check instance null or not
        public static void SaveCurrentGame(string fileName, List<string> steps, int playerMode)
        {
            // control only initial 1 instance through program
            if (_instance == null)
            {
                _instance = new GameLog();
            }
            _instance.SaveGame(fileName, steps, playerMode);
        }

        // Load game from log file
        public static List<string> LoadSavedGame(string fileName)
        {
            List<string> steps = new List<string>();
            // control only initial 1 instance through program
            if (_instance == null)
            {
                _instance = new GameLog();
            }

            steps = _instance.LoadFile(fileName);
            return steps;
        }

        private List<String> LoadFile(string fileName)
        {
            // select current file path
            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = currentDir + fileName;
            List<string> steps = new List<string>();

            try
            {
                var lines = File.ReadLines(filePath + ".txt");
                foreach (var line in lines)
                {
                    steps.Add(line);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Sorry! You don't have any saved game\n");
            }

            return steps;
        }
        // Argument
        // playerMode: Computer x Human (1), Human x Human (2)
        // This function: SaveGame into a log file
        // Will check current directory for log file, if it has => delete then save new log file
        private void SaveGame(string fileName, List<string> steps, int playerMode)
        {
            // select current file path
            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = currentDir + fileName;
            StringBuilder sb = new StringBuilder();

            try
            {
                // Check if has any log files. Delete if exist
                if (File.Exists(filePath + ".txt")) File.Delete(filePath + ".txt");

                // Start save game into log file
                sb.Append(playerMode);
                sb.Append("\n");
                if (steps != null)
                {
                    foreach (var item in steps)
                    {
                        sb.Append(item);
                        sb.Append("\n");
                    }

                }
                File.AppendAllText(filePath + ".txt", sb.ToString());
                sb.Clear();
                WriteLine("File path " + filePath);
                WriteLine("Write file successful");
            }
            catch (DirectoryNotFoundException)
            {
                WriteLine("Invalid File path");
                return;
            }
        }
    }
}
