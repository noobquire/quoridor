using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using System;
using System.Linq;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Controller
{
    public class ConsoleInput
    {
        public void StartProcessing(Game game)
        {
            string command;
            Console.WriteLine("Welcome to quoridor game!");
            Console.WriteLine("Type 'start' to start a new game.");
            while (true)
            {
                Console.WriteLine();
                command = Console.ReadLine();
                var splitCommand = command.Split(Array.Empty<char>());
                try
                {
                    switch (splitCommand[0].ToLower())
                    {
                        case "start":
                            game.Start();
                            break;
                        case "exit":
                            return;
                        case "move":
                            var moveX = int.Parse(splitCommand[1]);
                            var moveY = int.Parse(splitCommand[2]);
                            game.Move(moveX, moveY);
                            break;
                        case "wall":
                            var wallX = int.Parse(splitCommand[1]);
                            var wallyY = int.Parse(splitCommand[2]);
                            var wallType = (WallType)Enum.Parse(typeof(WallType), splitCommand[3]);
                            game.SetWall(wallType, wallX, wallyY);
                            break;
                    }
                }
                catch (QuoridorGameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Invalid number of arguments");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
