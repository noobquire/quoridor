using QuoridorGame.Model.Entities;
using System;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Controller
{
    public class ConsoleInput
    {
        public void StartProcessing(Game game)
        {
            string command;
            Console.WriteLine("Welcome to quorridor game!");
            while (true)
            {
                command = Console.ReadLine();
                var splitCommand = command.Split(Array.Empty<char>());
                switch (command)
                {
                    case "start":
                        game.Start();
                        break;
                    case "exit":
                        break;
                    case "move":
                        var moveX = int.Parse(splitCommand[1]);
                        var moveY = int.Parse(splitCommand[2]);
                        game.Move(moveX, moveY);
                        break;
                    case "place wall":
                        var wallX = int.Parse(splitCommand[1]);
                        var wallyY = int.Parse(splitCommand[2]);
                        var wallType = (WallType) Enum.Parse(typeof(WallType), splitCommand[3]);
                        game.SetWall(wallType, wallX, wallyY);
                        break;
                }
            }
        }
    }
}
