﻿using Microsoft.Extensions.DependencyInjection;
using QuoridorGame.Controller;
using QuoridorGame.Model.Entities;
using QuoridorGame.Model.Interfaces;
using QuoridorGame.Model.Logic;
using QuoridorGame.View;
using Game = QuoridorGame.Model.Entities.QuoridorGame;

namespace QuoridorGame
{
    internal class Program
    {
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            var game = new Game();
            serviceCollection.AddSingleton(game)
                             .AddScoped<IGraph<Cell>>(c => game.GameField.Cells);
            ConfigureServices(serviceCollection);
            var output = new ConsoleOutput();
            output.ListenTo(game);
            var input = new ConsoleInput();
            input.StartProcessing(game);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
            .AddScoped<IPathFinder<CellField, Cell>, PathFinder<CellField, Cell>>()
            .AddScoped<IWallPlacer, WallPlacer>()
            .AddScoped<IMovementLogic, MovementLogic>()
            .AddScoped<IQuoridorGameFacade, QuoridorGameFacade>();
        }
    }
}
