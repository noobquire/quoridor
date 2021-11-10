using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Exceptions;
using System;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame.Controller
{
    public class ConsoleInput
    {
        public void StartProcessing(Game game)
        {
            string command;
            while (true)
            {
                Console.WriteLine();
                command = Console.ReadLine();
                var splitCommand = command.Split(Array.Empty<char>());
                try
                {
                    switch (splitCommand[0].ToLower())
                    {
                        case "black":
                            // start as player 1, our bot plays as 2
                            //bot.ChoosePlayer(Player.White);
                            game.Start();
                            break;
                        case "white":
                            // start as player 2, our bot plays as 1
                            //bot.ChoosePlayer(Player.White);
                            game.Start();
                            break;
                        case "jump":
                        case "move":
                            var jumpCoords = ParseJumpCoordinates(splitCommand[1]);
                            game.Move(jumpCoords.x, jumpCoords.y);
                            break;
                        case "wall":
                            var wallCoords = ParseWallCoordinates(splitCommand[1]);
                            game.SetWall(wallCoords.wallType, wallCoords.x, wallCoords.y);
                            break;
                        default:
                            Console.WriteLine("Unknown command");
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
                catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
        
        public (int x, int y) ParseJumpCoordinates(string jumpCoordinates)
        {
            char horizontalChar = jumpCoordinates[0];
            if(horizontalChar < 'A' || horizontalChar > 'I')
            {
                throw new QuoridorGameException("Invalid coordinate");
            }
            char verticalChar = jumpCoordinates[1];
            if(verticalChar < '1' || verticalChar > '9')
            {
                throw new QuoridorGameException("Invalid coordinate");
            }

            int y = horizontalChar - 'A';
            int x = verticalChar - '1';
            return (x, y);
        }

        public (int x, int y, WallType wallType) ParseWallCoordinates(string wallCoordinates)
        {
            char horizontalChar = wallCoordinates[0];
            if (horizontalChar < 'S' || horizontalChar > 'Z')
            {
                throw new QuoridorGameException("Invalid coordinate");
            }
            char verticalChar = wallCoordinates[1];
            if (verticalChar < '1' || verticalChar > '8')
            {
                throw new QuoridorGameException("Invalid coordinate");
            }
            char wallType = wallCoordinates[2];
            if(wallType  != 'v' && wallType != 'h')
            {
                throw new QuoridorGameException("Invalid wall type");
            }

            int y = horizontalChar - 'S';
            int x = verticalChar - '1';
            var type = wallType == 'v' ? WallType.Vertical : WallType.Horizontal;
            return (x, y, type);
        }
    }
}
